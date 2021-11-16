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
        static Thread thrRecv;
        static bool IsUdpcRecvStart = false;
        static bool IsUdpcRecvSuccess = false;
        static byte[] udpRecv = new byte[56];

        static bool IsStart = false ;
        Device[] device = new Device[1000];
        static int index = 0;
        public DeviceExplorer()
        {
            InitializeComponent();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            if(IsStart == true)
            {
                MessageBox.Show("请不要重复开始！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                IsStart = true;
                int item = ComboBoxBrowseMode.SelectedIndex;
                switch (item)
                {
                    case 0:
                        {
                            this.LabelStatusText.Text = "发送中……";
                            localIpep = new IPEndPoint(BIA.Value, 44816); // 本机IP和监听端口号
                            udpcSend = new UdpClient(localIpep);
                            byte[] sendbytes = { 0x63, 00, 00, 00, 00, 00, 00,
                            00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00,
                            00, 00, 00, 00, 00 };
                            IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 44818); // 发送到的IP地址和端口号
                            udpcSend.Send(sendbytes, sendbytes.Length, remoteIpep);
                            udpcSend.Close();
                            byte[] udpRecv = new byte [56] ;
                            localIpep = new IPEndPoint(BIA.Value, 44816); // 本机IP和监听端口号
                            udpcRecv = new UdpClient(localIpep);
                            thrRecv = new Thread(ReceiveMessage);

                            thrRecv.Start();
                            IsUdpcRecvStart = true;
                            this.LabelStatusText.Text = "UDP监听器已成功启动,接受中……";


                            if (IsUdpcRecvSuccess == true)
                            {
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
        static private void ReceiveMessage(object obj)
        {
            while (IsUdpcRecvStart)
            {
                try
                {
                    udpRecv = udpcRecv.Receive(ref localIpep);
                    //确定收到无误
                    IsUdpcRecvSuccess = true;
                    
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
                thrRecv.Abort();
                udpcRecv.Close();
                this.LabelStatusText.Text = "成功关闭";
            }
            
        }

        public void udpDiswrap() 
        {
            IPAddress ip = localIpep.Address;
            device[index].ip = ip.ToString();

        }

    }
}

class Device
{
    public
    String ip;
    int Vendor;
    int ProductType;
    int ProductCode;
    String DeviceName;
    int SerialNumber;
}