﻿using System;
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

        static bool IsStart = false;                        // 是否已经开始
        Device[] device = new Device[MaxDevicesNumber];     // device对象数组
        static int index = 0;                               // 静态变量-设备数

        static Thread clock;                                // 计时器线程
        static bool needToFlush = false;                    // 是否到时间需要刷新
        public DeviceExplorer()
        {
            InitializeComponent();
        }
        // Evet定义=======================================================

        //开始单击Event
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            ButtonStop.Enabled = true;
            ButtonStart.Enabled = false;

            index = 0;
            this.treeView1.Nodes.Clear();
            //模式选择 0 1 2 3
            int item = ComboBoxBrowseMode.SelectedIndex;
            switch (item)
            {
                // 本地广播
                case 0:
                    {
                        needToFlush = true;

                        // 计时器线程
                        clock = new Thread(clockCount);
                        clock.Start();

                        // 状态变更
                        this.LabelStatusText.Text = "发送中……";

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
                                if (index >= MaxDevicesNumber) index = 0;

                                // 获取主机名
                                string name = getHostNameFun(BIA.Value.ToString());

                                // 将设备添加到treeview中
                                addItemTree(name,device[index]);
                                index++;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        finally
                        {
                            // 显示共捕捉输出设备数
                            this.LabelDevicesNumber.Text = index.ToString();
                            this.LabelStatusText.Text = "接收成功！已接受数目：" + index.ToString();
                            needToFlush = false;
                            // 关闭udp连接
                            udpConnect.Close();
                        }
                        break;
                    }
                case 1:
                    {
                        // 统计同游多少条需要发送的ip并转化为数组
                        int sumIp = ListBoxPointToPointIPAddress.Items.Count;
                        String[] ipString = new string[sumIp];

                        // 开启定时器
                        clock = new Thread(clockCount);
                        clock.Start();

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
                            // 配置发送Udp
                            localIpep = new IPEndPoint(BIA.Value, 44814); // 本机IP和监听端口号
                            udpConnect = new UdpClient(localIpep);
                            udpConnect.Client.ReceiveTimeout = 100;
                            byte[] sendbytes = { 0x63, 00, 00, 00, 00, 00, 00,
                            00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00,
                            00, 00, 00, 00, 00 };
                            //发送
                            IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse(ipString[i]), 44818); // 发送到的IP地址和端口号
                            udpConnect.Send(sendbytes, sendbytes.Length, remoteIpep);

                            try
                            {
                                // 接收udp，捕捉接收超时Exception
                                udpRecv = udpConnect.Receive(ref localIpep);
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }

                            IPAddress ip = localIpep.Address;

                            // 对接受进行解码
                            packageUnwarp(ref device[index], udpRecv, ip);

                            if (index >= 1000) index = 0;

                            string name = getHostNameFun(BIA.Value.ToString());

                            addItemTree(name,device[index]);

                            udpConnect.Close();
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
                        // 计时器线程
                        clock = new Thread(clockCount);
                        clock.Start();

                        // 清空设备列表
                        this.treeView1.Nodes.Clear();

                        // 状态变更
                        this.LabelStatusText.Text = "发送中……";

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
                                if (index >= MaxDevicesNumber) index = 0;

                                // 将设备添加到listbox中
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
                            // 显示共捕捉输出设备数
                            this.LabelDevicesNumber.Text = index.ToString();
                            this.LabelStatusText.Text = "接收成功！已接受数目：" + index.ToString();
                            needToFlush = false;
                            // 关闭udp连接
                            udpConnect.Close();
                        }
                        break;
                    }
                default: break;
            }
        }

        

        // 停止按钮的Event
        private void ButtonStop_Click(object sender, EventArgs e)
        {
            ButtonStop.Enabled = false;
            ButtonStart.Enabled = true;
            // 关闭定时器线程
            clock.Abort();
            IsStart = false;
            this.LabelStatusText.Text = "成功关闭";

        }



        // 清理按钮单击Event
        private void ButtonClear_Click(object sender, EventArgs e)
        {
            // 清空listbox内容
            this.treeView1.Nodes.Clear();
        }

        // listbox双击事件
        private void listBoxDevices_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // get双击item的index
            int index = this.ListBoxDevices.IndexFromPoint(e.Location);
            // get -1 即啥也没点到
            if (index == -1)
            {
                return;
            }

            // 展示device的属性
            MessageBox.Show(
                "Address:               " + device[index].ips + "\n" +
                "Vendor:                " + device[index].Vendor + "\n" +
                "Product Type:      " + device[index].ProductType + "\n" +
                "Product Code       " + device[index].ProductCode + "\n" +
                "Device Name:       " + device[index].DeviceName + "\n" +
                "Serial Number:     " + device[index].SerialNumber + "\n"

                , "Device Properties", MessageBoxButtons.OK, MessageBoxIcon.Information
                );
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


        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)  //单击鼠标左键才响应
            {
                if (e.Node.Level == 1)                               //判断子节点才响应
                {
                    // get双击item的index
                    int index = e.Node.Index;
                    // get -1 即啥也没点到
                    if (index == -1)
                    {
                        return;
                    }

                    // 展示device的属性
                    MessageBox.Show(
                        "Address:               " + device[index].ips + "\n" +
                        "Vendor:                " + device[index].Vendor + "\n" +
                        "Product Type:      " + device[index].ProductType + "\n" +
                        "Product Code       " + device[index].ProductCode + "\n" +
                        "Device Name:       " + device[index].DeviceName + "\n" +
                        "Serial Number:     " + device[index].SerialNumber + "\n"

                        , "Device Properties", MessageBoxButtons.OK, MessageBoxIcon.Information
                        );
                }

            }
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

        private void addItemTree(String parent,Device device)
        {
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
            treeView1.ExpandAll();
            treeView1.SelectedNode.Nodes.Add(device.ips + "-" + device.DeviceName, device.ips + "-" + device.DeviceName,1,1);
        }

        private string getHostNameFun(String ip)
        {
            // 获取主机名
            IPHostEntry myscanhost = Dns.GetHostEntry(
            IPAddress.Parse(ip));
            string name = myscanhost.HostName.ToString();
            return name;
        }

        // 线程定义=======================================================
        // 时钟线程
        public void clockCount()
        {
            // 线程休眠Time，之后改变状态
            while (true)
            {
                Thread.Sleep(ClockCountTime);
                index = 0;
            }

        }

        private void ComboBoxBrowseMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int item = ComboBoxBrowseMode.SelectedIndex;
            switch (item)
            {
                case 0:
                    {
                        ipAddressTextBox1.Enabled = false;
                        ButtonAdd.Enabled = false;
                        ButtonRemove.Enabled = false;
                        RIA.Enabled = false;
                        RSM.Enabled = false;
                        break;
                    }
                case 1:
                    {
                        ipAddressTextBox1.Enabled = true;
                        ButtonAdd.Enabled = true;
                        ButtonRemove.Enabled = true;
                        RIA.Enabled = false;
                        RSM.Enabled = false;
                        break;
                    }
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

        private void DeviceExplorer_Load(object sender, EventArgs e)
        {
            ComboBoxBrowseMode.SelectedIndex = 0;
        }
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