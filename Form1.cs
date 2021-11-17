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
        static IPEndPoint localIpep = null;
        private static UdpClient udpcSend = null;
        static byte[] udpRecv = new byte[1024000];

        public const int MaxDevicesNumber = 3000;   // public yyds 最大设备数
        public int ClockCountTime = 5000;    // 计时器间隔

        static bool IsStart = false;
        Device[] device = new Device[MaxDevicesNumber];
        static int index = 0;

        static Thread clock;
        static bool needToFlush = false;
        public DeviceExplorer()
        {
            InitializeComponent();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            // 如已经开启，抛出警报
            if (IsStart == true)
            {
                MessageBox.Show("请先结束！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                IsStart = true;
                index = 0;
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

                            // 清空设备列表
                            this.ListBoxDevices.Items.Clear();

                            // 状态变更
                            this.LabelStatusText.Text = "发送中……";

                            // 本机IP和监听端口号
                            localIpep = new IPEndPoint(BIA.Value, 44814);

                            // 发送UDP访问报文
                            udpcSend = new UdpClient(localIpep);
                            udpcSend.Client.ReceiveTimeout = 100;
                            byte[] sendbytes = { 0x63, 00, 00, 00, 00, 00, 00,
                                00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00,
                                00, 00, 00, 00, 00 };

                            IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 44818); // 发送到的IP地址和端口号
                            udpcSend.Send(sendbytes, sendbytes.Length, remoteIpep);

                            // 接收回传报文
                            try
                            {
                                while (true)
                                {
                                    // udpRecv接收数组，单次接收
                                    // 读取缓冲区数据
                                    udpRecv = udpcSend.Receive(ref localIpep);
                                    IPAddress ip = localIpep.Address;

                                    // 数组解包
                                    packageUnwarp(ref device[index], udpRecv, ip);


                                    // 当index大于最大设备数组，重置index
                                    if (index >= MaxDevicesNumber) index = 0;

                                    // 将设备添加到listbox中
                                    this.ListBoxDevices.Items.Add(device[index].ips + "-" + device[index].DeviceName);
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
                                udpcSend.Close();
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
                            this.ListBoxDevices.Items.Clear();
                            this.LabelStatusText.Text = "发送中……";

                            // 向每个目标发送udp
                            for (int i = 0; i < sumIp; i++)
                            {
                                // 配置发送Udp
                                localIpep = new IPEndPoint(BIA.Value, 44814); // 本机IP和监听端口号
                                udpcSend = new UdpClient(localIpep);
                                udpcSend.Client.ReceiveTimeout = 100;
                                byte[] sendbytes = { 0x63, 00, 00, 00, 00, 00, 00,
                                00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00,
                                00, 00, 00, 00, 00 };
                                //发送
                                IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse(ipString[i]), 44818); // 发送到的IP地址和端口号
                                udpcSend.Send(sendbytes, sendbytes.Length, remoteIpep);

                                try
                                {
                                    // 接收udp，捕捉接收超时Exception
                                    udpRecv = udpcSend.Receive(ref localIpep);
                                }
                                catch (Exception ex)
                                {
                                    continue;
                                }

                                IPAddress ip = localIpep.Address;

                                // 对接受进行解码
                                packageUnwarp(ref device[index], udpRecv, ip);

                                if (index >= 1000) index = 0;

                                // 添加入listbox
                                this.ListBoxDevices.Items.Add(device[index].ips + "-" + device[index].DeviceName);
                                index++;
                                this.LabelDevicesNumber.Text = index.ToString();

                                needToFlush = false;
                                udpcSend.Close();
                            }

                            this.LabelStatusText.Text = "接收成功！已接受数目：" + index.ToString();
                            this.LabelDevicesNumber.Text = index.ToString();


                            break;
                        }
                    case 2:
                        {
                            String[] mask = RSM.Value.ToString().Split('.');
                            String ip1 = Convert.ToString(Convert.ToInt32(mask[0]), 2);
                            String ip2 = Convert.ToString(Convert.ToInt32(mask[1]), 2);
                            String ip3 = Convert.ToString(Convert.ToInt32(mask[2]), 2);
                            String ip4 = Convert.ToString(Convert.ToInt32(mask[3]), 2);
                            int countNum = 0;
                            bool isFind = false;

                            foreach(char i in ip1+ip2+ip3+ip4)
                            {
                                if (i == '0')
                                {
                                    isFind = true;
                                    break;
                                }    
                                countNum++;
                            }



                            break;
                        }
                    default: break;
                }

            }
        }

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

            // device.ProductType
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            if (IsStart == false)
            {
                MessageBox.Show("尚未启动！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                clock.Abort();
                IsStart = false;
                this.LabelStatusText.Text = "成功关闭";
            }

        }

        public void udpDiswrap(byte[] b)
        {
            IPAddress ip = localIpep.Address;
            device[index] = new Device();
            device[index++].ips = ip.ToString();
            if (index >= MaxDevicesNumber) index = 0;


        }

        public void clockCount()
        {
            Thread.Sleep(ClockCountTime);
            index = 0;
        }
        private void ButtonClear_Click(object sender, EventArgs e)
        {
            this.ListBoxDevices.Items.Clear();
        }
        private void listBoxDevices_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.ListBoxDevices.IndexFromPoint(e.Location);
            if (index == -1)
            {
                return;
            }
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

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            IPAddress ip = ipAddressTextBox1.Value;
            if (ip.ToString() == "127.0.0.1")
            {
                MessageBox.Show("禁止回环发送！", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ListBoxPointToPointIPAddress.Items.Contains(ip.ToString()))
                this.ListBoxPointToPointIPAddress.Items.Add(ip.ToString());
        }

        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            ListBoxPointToPointIPAddress.Items.Remove(ListBoxPointToPointIPAddress.SelectedItem);
        }

        private void ButtonTimeSet_Click(object sender, EventArgs e)
        {
            try
            {
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
            catch (FormatException fe)
            {
                MessageBox.Show("格式错误！", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }


    }
}

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