using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
    /// ClientWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ThreadClientWindow : Window
    {
        private bool isExit = false;
        private TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;
        private string remoteHost;
        private int remotePort = 53985;

        public ThreadClientWindow()
        {
            InitializeComponent();
            this.Closing += ClientWindow_Closing;
            remoteHost = Dns.GetHostName();
        }

        void ClientWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (client != null)
            {
                SendMessage("Logout," + textBoxUserName.Text);
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
                btnLogin.IsEnabled = true;
            }
            catch
            {
                AddInfo("与服务端连接失败");
                return;
            }
            NetworkStream networkStream = client.GetStream();
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
            SendMessage("Login," + textBoxUserName.Text);
            Thread threadReceive = new Thread(ReceiveData);
            threadReceive.IsBackground = true;
            threadReceive.Start();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            int idx=listboxNames.SelectedIndex;
            if (idx != -1)
                SendMessage("TalkToOne," + listboxNames.Items[idx]+","+ textBoxSend.Text);
            else 
            SendMessage("Talk," + textBoxSend.Text);
            textBoxSend.Clear();
        }

        /// <summary>处理接收的服务器端数据</summary>
        private void ReceiveData()
        {
            string receiveString = null;
            while (isExit == false)
            {
                try { 
                    receiveString = br.ReadString();
                    string[] split = receiveString.Split(',');
                    switch (split[0])
                    {
                        case "List":  
                            
                            Console.WriteLine(receiveString);
                         
                            for (int i = 1; i < split.Length; i++)
                            {

                                 listboxNames.Dispatcher.Invoke(() =>
                                {
                                    this.listboxNames.Items.Add(split[i]);
                                });
                            }
                            break;
                        case "Logout":  
                            listboxNames.Dispatcher.Invoke(() =>
                            {
                                this.listboxNames.Items.Remove(split[1]);
                            });
                            break;
                        default:
                            break;
                            throw new Exception("无法解析：" + receiveString);
                    }
                }
                catch
                {
                    if (isExit == true) AddInfo("与服务器失去联系。");
                    break;
                }
                AddInfo(receiveString);
            }
        }

        /// <summary>向服务器端发送信息</summary>
        private void SendMessage(string message)
        {
            try { bw.Write(message); bw.Flush(); }
            catch { AddInfo("发送失败!"); }
        }
    }
}
