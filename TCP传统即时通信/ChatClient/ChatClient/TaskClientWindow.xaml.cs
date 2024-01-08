using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatClient
{
    /// <summary>
    /// TaskClientWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TaskClientWindow : Window
    {
        private bool isExit = false;
        private TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;
        private string remoteHost;
        private int remotePort = 53985;

        public TaskClientWindow()
        {
            InitializeComponent();
            this.Closing += ClientWindow_Closing;
            remoteHost = Dns.GetHostName();
        }

        void ClientWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (client != null)
            {
                SendMessage("Logout," + textBoxUserName.Text);//格式：Logout,用户名
                isExit = true;
                br.Close();
                bw.Close();
                client.Close();
            }
        }

        private void AddInfo(string format, params object[] args)
        {
            textBlock1.Dispatcher.InvokeAsync(() =>
            {
                textBlock1.Text += string.Format(format, args) + "\n";
            });
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                client = new TcpClient(remoteHost, remotePort);
                AddInfo("与服务端连接成功");
                btnLogin.IsEnabled = false;
            }
            catch
            {
                AddInfo("与服务端连接失败");
                return;
            }
            //获取网络流
            NetworkStream networkStream = client.GetStream();
            //将网络流作为二进制读写对象
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
            SendMessage("Login," + textBoxUserName.Text);
            Task.Run(() => ReceiveData());
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            SendMessage("Talk," + textBoxSend.Text);
            textBoxSend.Clear();
        }

        /// <summary>处理接收的数据</summary>
        private void ReceiveData()
        {
            string receiveString = null;
            while (isExit == false)
            {
                try
                {
                    //从网络流中读出字符串
                    //此方法会自动判断字符串长度前缀，并根据长度前缀读出字符串
                    receiveString = br.ReadString();
                }
                catch
                {
                    if (isExit == false)
                    {
                        AddInfo("与服务端失去联系。");
                    }
                    break;
                }
                AddInfo(receiveString);
            }
           // this.Close();
        }

        /// <summary>向服务器端发送信息</summary>
        private void SendMessage(string message)
        {
            try
            {
                //将字符串写入网络流，此方法会自动附加字符串长度前缀
                bw.Write(message);
                bw.Flush();
            }
            catch
            {
                AddInfo("发送失败!");
            }
        }
    }
}
