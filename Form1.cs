using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;




namespace DeviceExplorer
{
    public partial class DeviceExplorer : Form
    {
        static IPEndPoint localIpep = null;
        private static UdpClient udpcSend = null;
        static UdpClient udpcRecv = null;
        static bool IsUdpcRecvStart = false;
        static bool IsUdpcRecvSuccess = false;
        static byte[] udpRecv = new byte[1024000];
        static byte[] udpRecv2 = new byte[1024000];
        static int countOfRecieve = 0;

        static bool IsStart = false;
        Device[] device = new Device[1000];
        static int index = 0;
        public DeviceExplorer()
        {
            InitializeComponent();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            if (IsStart == true)
            {
                MessageBox.Show("请不要重复开始！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                IsStart = true;
                index = 0;
                int item = ComboBoxBrowseMode.SelectedIndex;
                countOfRecieve = 0;
                switch (item)
                {
                    case 0:
                        {

                            this.ListBoxDevices.Items.Clear();
                            this.LabelStatusText.Text = "发送中……";
                            localIpep = new IPEndPoint(BIA.Value, 44814); // 本机IP和监听端口号
                            udpcSend = new UdpClient(localIpep);
                            udpcSend.Client.ReceiveTimeout = 10;
                            byte[] sendbytes = { 0x63, 00, 00, 00, 00, 00, 00,
                            00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00,
                            00, 00, 00, 00, 00 };
                            IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 44818); // 发送到的IP地址和端口号
                            udpcSend.Send(sendbytes, sendbytes.Length, remoteIpep);
                            try
                            {
                                while (true)
                                {
                                    udpRecv = udpcSend.Receive(ref localIpep);
                                    IPAddress ip = localIpep.Address;
                                    device[index] = new Device();
                                    device[index].ips = ip.ToString();
                                    device[index].Vendor = udpRecv[48] + udpRecv[49] * 16;
                                    device[index].SerialNumber = "0x" +
                                        Convert.ToString(udpRecv[61], 16) +
                                        Convert.ToString(udpRecv[60], 16) +
                                        Convert.ToString(udpRecv[59], 16) +
                                        Convert.ToString(udpRecv[58], 16);
                                    if (index >= 1000) index = 0;
                                    this.ListBoxDevices.Items.Add(device[index].ips + "/");
                                    index++;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            finally
                            {
                                /*udpcSend.Close();
                                localIpep = new IPEndPoint(BIA.Value, 44814); // 本机IP和监听端口号
                                udpcRecv = new UdpClient(localIpep);
                                thrRecv = new Thread(ReceiveMessage);
                                thrRecv.Start();
                                IsUdpcRecvStart = true;
                                this.LabelStatusText.Text = "UDP监听器已成功启动,接受中……";
                                */
                                this.LabelDevicesNumber.Text = index.ToString();
                                this.LabelStatusText.Text = "接收成功！";

                            }
                            break;




                        }
                    case 1:
                        {
                            break;
                        }
                    case 2:
                        {
                            break;
                        }
                    default: break;
                }

            }



        }
        private void ReceiveMessage(object obj)
        {
            while (IsUdpcRecvStart)
            {
                try
                {
                    byte[] b = new byte[100];
                    if (index < 200)
                    {
                        udpRecv = udpcRecv.Receive(ref localIpep);
                    }
                    else
                    {
                        udpRecv2 = udpcRecv.Receive(ref localIpep);
                    }

                    //确定收到无误
                    IsUdpcRecvSuccess = true;
                    udpDiswrap(b);
                    countOfRecieve++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
            }
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            if (IsStart == false)
            {
                MessageBox.Show("尚未启动！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                IsStart = false;
                udpcSend.Close();
                this.LabelStatusText.Text = "成功关闭";
            }

        }

        public void udpDiswrap(byte[] b)
        {
            IPAddress ip = localIpep.Address;
            device[index] = new Device();
            device[index++].ips = ip.ToString();
            if (index >= 1000) index = 0;


        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            this.ListBoxDevices.Items.Clear();
        }

        private void ipAddressTextBox2_Load(object sender, EventArgs e)
        {

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