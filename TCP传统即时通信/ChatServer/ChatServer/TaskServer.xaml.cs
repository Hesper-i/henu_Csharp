using System;
using System.Collections.Generic;
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

namespace ChatServer
{
    /// <summary>
    /// TaskAsyncServer.xaml 的交互逻辑
    /// </summary>
    public partial class TaskServer : Window
    {
        private TcpListener myListener;
        public TaskServer()
        {
            InitializeComponent();
            this.Closing += TaskServer_Closing;
            btnStop.IsEnabled = false;
        }

        void TaskServer_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (myListener != null) myListener.Stop();
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            CC.ChangeState(btnStart, false, btnStop, true);
            myListener = new TcpListener(CC.localIP, CC.port);
            myListener.Start();
            textBlock1.Text += string.Format("开始在{0}:{1}监听客户连接",
                CC.localIP, CC.port);
            while (true)
            {
                try
                {
                    TcpClient newClient = await myListener.AcceptTcpClientAsync();
                    User user = new User(newClient,true);
                    CC.userList.Add(user);
                }
                catch { break; }
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            myListener.Stop();
            textBlock1.Text += "\n监听已停止!";
            CC.ChangeState(btnStart, true, btnStop, false);
        }
    }
}
