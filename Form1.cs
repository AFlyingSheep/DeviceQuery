using System;
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
            if (IsStart == true)
            {
                MessageBox.Show("请不要重复开始！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                IsStart = true;
                index = 0;
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
                                    udpRecv = udpcSend.Receive(ref localIpep);
                                    IPAddress ip = localIpep.Address;

                                    packageUnwarp(ref device[index], udpRecv, ip);

                                    // 重置？把前面的挤掉？
                                    if (index >= MaxDevicesNumber) index = 0;
                                    
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
                                this.LabelDevicesNumber.Text = index.ToString();
                                this.LabelStatusText.Text = "接收成功！";
                                needToFlush = false;
                                udpcSend.Close();
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

        private String byteToString(byte [] vs, int begin , int end)
        {
            String resultString = null;
            byte[] result = vs.Skip(begin).Take(end-begin).ToArray();
            resultString += Encoding.ASCII.GetString(result);
            return resultString;
        }

        // UDP解包
        private void packageUnwarp(ref Device device, byte[] vs,IPAddress ip)
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
                "Device Name:       " + device[index].DeviceName+ "\n" +
                "Serial Number:     " + device[index].SerialNumber + "\n"

                , "Device Properties", MessageBoxButtons.OK, MessageBoxIcon.Information
                ) ;
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