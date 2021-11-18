using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace DeviceExplorer
{
    public partial class DeviceExplorer : Form
    {
        // 类全局变量定义==================================================
        static IPEndPoint localIpep = null;                 // IP配置
        private static UdpClient udpConnect = null;         // udp连接
        static byte[] udpRecv = new byte[100];              // 接收缓冲区

        public const int MaxDevicesNumber = 3000;           // public yyds 最大设备数
        public int ClockCountTime = 5000;                   // 计时器间隔

        Device[] device = new Device[MaxDevicesNumber];     // device对象数组
        static int index = 0;                               // 静态变量-设备数

        public DeviceExplorer()
        {
            InitializeComponent();
        }
        // Event定义=======================================================

        //开始单击Event
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            // 按钮限制
            ButtonStop.Enabled = true;
            ButtonStart.Enabled = false;
            ComboBoxBrowseMode.Enabled = false;
            TextBoxTimeSet.Enabled = false;
            ButtonTimeSet.Enabled = false;
            RefreshButton.Enabled = false;
            BIA.Enabled = false;


            index = 0;
            this.treeView1.Nodes.Clear();

            //定时器开始
            timer1.Interval = ClockCountTime;
            timer1.Enabled = true;
            timer1.Start();

            //模式选择 0 1 2 3
            int item = ComboBoxBrowseMode.SelectedIndex;
            switch (item)
            {
                // 本地广播
                case 0:
                    {
                        // 状态变更
                        this.LabelStatusText.Text = "发送中……";
                        case0SendAndRecieve();
                        // 显示共捕捉输出设备数
                        this.LabelDevicesNumber.Text = index.ToString();
                        this.LabelStatusText.Text = "接收成功！已接受数目：" + index.ToString();

                        break;
                    }
                case 1:
                    {
                        // 统计同游多少条需要发送的ip并转化为数组
                        int sumIp = ListBoxPointToPointIPAddress.Items.Count;
                        String[] ipString = new string[sumIp];

                        // 将ip转化为string存入字符串数组
                        for (int i = 0; i < sumIp; i++)
                        {
                            ipString[i] = ListBoxPointToPointIPAddress.Items[i].ToString();
                        }

                        // 更改状态
                        this.treeView1.Nodes.Clear();
                        this.LabelStatusText.Text = "发送中……";

                        // 向每个目标发送udp
                        for (int i = 0; i < sumIp; i++)
                        {
                            case1SendAndRecieve(ipString[i]);
                        }

                        this.LabelStatusText.Text = "接收成功！已接受数目：" + index.ToString();
                        this.LabelDevicesNumber.Text = index.ToString();


                        break;
                    }
                case 2:
                    {
                        //子网掩码、IP及广播地址的计算
                        String mask = RSM.Value.ToString();
                        String remoteIp = RIA.Value.ToString();
                        String boardcastIP = GetBroadcast(remoteIp, mask);

                        // 清空设备列表
                        this.treeView1.Nodes.Clear();

                        // 状态变更
                        this.LabelStatusText.Text = "发送中……";

                        case2SendAndRecieve();

                        // 显示共捕捉输出设备数
                        this.LabelDevicesNumber.Text = index.ToString();
                        this.LabelStatusText.Text = "接收成功！已接受数目：" + index.ToString();

                        break;
                    }
                default: break;
            }
        }

        

        // 停止按钮的Event
        private void ButtonStop_Click(object sender, EventArgs e)
        {
            RefreshButton.Enabled = false;
            ButtonStop.Enabled = false;
            ButtonStart.Enabled = true;
            ComboBoxBrowseMode.Enabled = true;
            TextBoxTimeSet.Enabled = true;
            ButtonTimeSet.Enabled = true;
            RefreshButton.Enabled = true;
            BIA.Enabled = true;

            // 关闭定时器线程
            this.LabelStatusText.Text = "成功关闭";
            timer1.Enabled = false;
            timer1.Stop();

        }

        // 清理按钮单击Event
        private void ButtonClear_Click(object sender, EventArgs e)
        {
            // 清空listbox内容
            this.treeView1.Nodes.Clear();
        }

        // 给定ip地址的模型-添加按钮单击Event
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            IPAddress ip = ipAddressTextBox1.Value;
            if (ip.ToString() == "127.0.0.1")
            {
                // 去掉回环发送，我也不知道为啥要去掉，其实不去掉没问题
                MessageBox.Show("禁止回环发送！", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 添加IP地址
            if (!ListBoxPointToPointIPAddress.Items.Contains(ip.ToString()))
                this.ListBoxPointToPointIPAddress.Items.Add(ip.ToString());
        }

        // 移除按钮单击Event
        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            ListBoxPointToPointIPAddress.Items.Remove(ListBoxPointToPointIPAddress.SelectedItem);
        }

        // 设置轮询时间按钮单击Event
        private void ButtonTimeSet_Click(object sender, EventArgs e)
        {
            // 捕捉用户非法输入
            try
            {
                // 轮询时间进行设置，防止轮询时间过快
                if (Convert.ToInt32(this.TextBoxTimeSet.Text) < 1000)
                {
                    MessageBox.Show("禁止小于1秒轮询！", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    ClockCountTime = Convert.ToInt32(this.TextBoxTimeSet.Text);
                    MessageBox.Show("设置成功！轮询时间：" + ClockCountTime.ToString(), "成功！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("格式错误！", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex);
                return;
            }
            catch(OverflowException ex)
            {
                MessageBox.Show("数值过大！", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex);
                return;
            }


        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)  //单击鼠标左键才响应
            {
                if (e.Node.Level == 1)                               //判断子节点才响应
                {
                    // 查询节点属性并展示
                    getProperties(e.Node);
                }

            }
        }

        // combobox更改时的定义，主要是对于按钮的enable
        private void ComboBoxBrowseMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int item = ComboBoxBrowseMode.SelectedIndex;
            switch (item)
            {
                // 本地
                case 0:
                    {
                        ipAddressTextBox1.Enabled = false;
                        ButtonAdd.Enabled = false;
                        ButtonRemove.Enabled = false;
                        RIA.Enabled = false;
                        RSM.Enabled = false;
                        break;
                    }

                // p2p
                case 1:
                    {
                        ipAddressTextBox1.Enabled = true;
                        ButtonAdd.Enabled = true;
                        ButtonRemove.Enabled = true;
                        RIA.Enabled = false;
                        RSM.Enabled = false;
                        break;
                    }

                // 远程子网
                case 2:
                    {
                        ipAddressTextBox1.Enabled = false;
                        ButtonAdd.Enabled = false;
                        ButtonRemove.Enabled = false;
                        RIA.Enabled = true;
                        RSM.Enabled = true;
                        break;
                    }
                default: break;
            }
        }

        // 在form加载时为combobox赋默认值
        private void DeviceExplorer_Load(object sender, EventArgs e)
        {
            ComboBoxBrowseMode.SelectedIndex = 0;

            //treeview1的排序
            treeView1.TreeViewNodeSorter = new NodeSorter();
        }


        // 三个case的执行后端执行代码==========================================================
        // case 0 的event
        private int case0SendAndRecieve()
        {
            // 本机IP和监听端口号
            localIpep = new IPEndPoint(BIA.Value, 44814);

            // 发送UDP访问报文
            udpConnect = new UdpClient(localIpep);
            udpConnect.Client.ReceiveTimeout = 100;
            byte[] sendbytes = { 0x63, 00, 00, 00, 00, 00, 00,
                            00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00,
                            00, 00, 00, 00, 00 };

            IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 44818); // 发送到的IP地址和端口号
            udpConnect.Send(sendbytes, sendbytes.Length, remoteIpep);

            // 接收回传报文
            try
            {
                while (true)
                {
                    // udpRecv接收数组，单次接收
                    // 读取缓冲区数据
                    udpRecv = udpConnect.Receive(ref localIpep);
                    IPAddress ip = localIpep.Address;

                    // 数组解包
                    packageUnwarp(ref device[index], udpRecv, ip);


                    // 当index大于最大设备数组，重置index
                    if (index >= MaxDevicesNumber)
                    {
                        MessageBox.Show("超出设备上限！", "警告",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        index = 0;
                    }

                    // 获取主机名
                    string name = getHostNameFun(BIA.Value.ToString());

                    // 将设备添加到treeview中
                    addItemTree(name, device[index]);
                    index++;

                    // 作用：防止界面假死
                    // 代价：增加加载时间
                    //Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                // 关闭udp连接
                udpConnect.Close();
            }
            return 0;
        }

        private int case1SendAndRecieve(String ipString)
        {
            // 配置发送Udp
            localIpep = new IPEndPoint(BIA.Value, 44814); // 本机IP和监听端口号
            udpConnect = new UdpClient(localIpep);
            udpConnect.Client.ReceiveTimeout = 100;
            byte[] sendbytes = { 0x63, 00, 00, 00, 00, 00, 00,
                            00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00,
                            00, 00, 00, 00, 00 };
            //发送
            IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse(ipString), 44818); // 发送到的IP地址和端口号
            udpConnect.Send(sendbytes, sendbytes.Length, remoteIpep);

            try
            {
                // 接收udp，捕捉接收超时Exception
                udpRecv = udpConnect.Receive(ref localIpep);
            }
            catch (Exception)
            {
                
            }
            finally
            {
                 IPAddress ip = localIpep.Address;

                // 对接受进行解码
                packageUnwarp(ref device[index], udpRecv, ip);
                string name = getHostNameFun(BIA.Value.ToString());
                addItemTree(name, device[index]);

                index++;
                if (index >= MaxDevicesNumber)
                {
                    MessageBox.Show("超出设备上限！", "警告",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    index = 0;
                }

                udpConnect.Close();
            }
            return 0;
        }

        private int case2SendAndRecieve()
        {
            // 本机IP和监听端口号
            localIpep = new IPEndPoint(BIA.Value, 44814);

            // 发送UDP访问报文
            udpConnect = new UdpClient(localIpep);
            udpConnect.Client.ReceiveTimeout = 100;
            byte[] sendbytes = { 0x63, 00, 00, 00, 00, 00, 00,
                            00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00,
                            00, 00, 00, 00, 00 };

            IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 44818); // 发送到的IP地址和端口号
            udpConnect.Send(sendbytes, sendbytes.Length, remoteIpep);

            // 接收回传报文
            try
            {
                while (true)
                {
                    // udpRecv接收数组，单次接收
                    // 读取缓冲区数据
                    udpRecv = udpConnect.Receive(ref localIpep);
                    IPAddress ip = localIpep.Address;

                    // 数组解包
                    packageUnwarp(ref device[index], udpRecv, ip);

                    // 当index大于最大设备数组，重置index
                    if (index >= MaxDevicesNumber)
                    {
                        MessageBox.Show("超出设备上限！", "警告",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        index = 0;
                    }
                    // 将设备添加到treeview中
                    string name = RIA.Value.ToString();
                    addItemTree(name, device[index]);
                    index++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                // 关闭udp连接
                udpConnect.Close();
            }
            return 0;
        }

        // 方法定义=======================================================
        // 远程子网广播地址计算
        public static string GetBroadcast(string ipAddress, string subnetMask)
            {

                byte[] ip = IPAddress.Parse(ipAddress).GetAddressBytes();
                byte[] sub = IPAddress.Parse(subnetMask).GetAddressBytes();

                // 广播地址=子网按位求反 再 或IP地址
                for (int i = 0; i < ip.Length; i++)
                {
                    ip[i] = (byte)((~sub[i]) | ip[i]);
                }
                return new IPAddress(ip).ToString();
            }


        // byte 2 字符串
        private String byteToString(byte[] vs, int begin, int end)
        {
            String resultString = null;
            byte[] result = vs.Skip(begin).Take(end - begin).ToArray();
            resultString += Encoding.ASCII.GetString(result);
            return resultString;
        }

        // UDP解包
        private void packageUnwarp(ref Device device, byte[] vs, IPAddress ip)
        {
            // 为device分配存储空间
            // 根据不同的缓冲区位 来解码
            device = new Device();
            device.ips = ip.ToString();
            device.Vendor = udpRecv[48] + udpRecv[49] * 16;
            device.ProductType = udpRecv[50] + udpRecv[51] * 16;
            device.ProductCode = udpRecv[52] + udpRecv[53] * 16;
            device.SerialNumber = "0x" +
                Convert.ToString(udpRecv[61], 16) +
                Convert.ToString(udpRecv[60], 16) +
                Convert.ToString(udpRecv[59], 16) +
                Convert.ToString(udpRecv[58], 16);
            device.DeviceName = byteToString(vs, 63, vs.Length - 1);
        }

        // treeView加入节点，父节点parent，子节点device
        private void addItemTree(String parent,Device device)
        {
            // 查看父节点是否存在 不存在则插入父节点
            bool find = false;
            foreach(TreeNode tn in treeView1.Nodes)
            {
                if(tn.Text == parent)
                {
                    find = true;
                }
            }
            if (!find)
            {
                treeView1.Nodes.Add(parent,parent,0);
            }

           
            treeView1.SelectedNode = treeView1.Nodes[0];
            // 父节点展开
            treeView1.ExpandAll();
            // 子节点插入
            treeView1.SelectedNode.Nodes.Add(device.ips + "-" + device.DeviceName, device.ips + "-" + device.DeviceName,1,1);
        }

        // 通过ip地址获得主机名称
        private string getHostNameFun(String ip)
        {
            // 获取主机名
            IPHostEntry myscanhost = Dns.GetHostEntry(
            IPAddress.Parse(ip));
            string name = myscanhost.HostName.ToString();
            return name;
        }

        // 查询节点属性并显示
        private void getProperties(TreeNode treeNode)
        {
            // 通过节点名获取ip地址
            String nodeIp = treeNode.Text.Split('-')[0];
            // 遍历设备列表，查找属性
            for(int i = 0; i < index; i++)
            {
                if (String.Equals(device[i].ips, nodeIp))
                {
                    MessageBox.Show(
                        "Address:               " + device[i].ips + "\n" +
                        "Vendor:                " + device[i].Vendor + "\n" +
                        "Product Type:      " + device[i].ProductType + "\n" +
                        "Product Code       " + device[i].ProductCode + "\n" +
                        "Device Name:       " + device[i].DeviceName + "\n" +
                        "Serial Number:     " + device[i].SerialNumber + "\n"

                        , "Device Properties", MessageBoxButtons.OK, MessageBoxIcon.Information
                        );
                    break;
                }
            }
        }


        // 线程定义=======================================================
        // 时钟定时器
        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshButton.Enabled = true;
            timer1.Stop();
        }

    }

    // 设备类
    class Device
    {
        public String ips;
        public int Vendor;
        public int ProductType;
        public int ProductCode;
        public String DeviceName;
        public String SerialNumber;
        public Device()
        {
            ips = null;
            Vendor = 0;
            ProductType = 0;
            ProductCode = 0;
            DeviceName = null;
            SerialNumber = null;
        }
    }

    //ip地址排序器 
    public class NodeSorter : IComparer
    {
        // 实现接口的compare方法
        public int Compare(object x,object y)
        {
            TreeNode tx = x as TreeNode;
            TreeNode ty = y as TreeNode;

            String[] txValue = tx.Text.Split('.');
            String[] tyValue = ty.Text.Split('.');
            txValue[3] = txValue[3].Split('-')[0];
            tyValue[3] = tyValue[3].Split('-')[0];
            for (int i = 0; i < 4; i++)
            {
                if(Convert.ToInt32(txValue[i])> Convert.ToInt32(tyValue[i]))
                {
                    return 1;
                }
                else if(Convert.ToInt32(txValue[i]) < Convert.ToInt32(tyValue[i]))
                {
                    return -1;
                }
            }
            return 0;
        }
    }
}