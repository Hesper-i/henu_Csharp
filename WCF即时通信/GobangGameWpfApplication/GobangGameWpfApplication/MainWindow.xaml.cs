using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GobangGameWpfApplication.ServiceReference1;

namespace GobangGameWpfApplication
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, IGobangServiceCallback
    {

        public MainWindow()
        {
            InitializeComponent();
            //
            this.Closing += Window_Closing;
            this.btnLogout.IsEnabled = false;
            this.btnSend.IsEnabled = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            client.Logout(UserName);
            e.Cancel = false;
        }

        public string UserName;
        private GobangServiceClient client; 


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            UserName = textBoxUserName.Text;
            if (textBoxUserName.Text == String.Empty)
            {
                MessageBox.Show("用户名不能为空！");
                return;
            }
            InstanceContext context = new InstanceContext(this);
            client = new GobangServiceClient(context);

            client.Login(textBoxUserName.Text);
            serviceTextBlock.Text = "服务端地址：" + client.Endpoint.ListenUri.ToString();
            this.dataGrid.ItemsSource = null;
            client.LoginList();

            this.btnLogin.IsEnabled = false;
            this.btnLogout.IsEnabled = true;
            this.btnSend.IsEnabled = true;

        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxSend.Text == String.Empty)
            {
                MessageBox.Show("发送的消息不能为空！");
                return;
            }
            client.sendInfo(textBoxSend.Text, UserName);
            textBoxSend.Clear();

        }
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxUserName.Text == String.Empty)
            {
                MessageBox.Show("用户名不能为空！");
                return;
            }
            client.Logout(UserName);
            userscount.Text = "在线人数：0";

            this.btnLogin.IsEnabled = true;
            this.btnLogout.IsEnabled = false;
            this.btnSend.IsEnabled = false;

        }

        public void ShowLogin(string loginUserName, int userCount)
        {
            listBoxMessage.Items.Add(loginUserName + "进入大厅。");
            userscount.Text = "在线人数：" +  userCount.ToString();
        }
        
        public void ShowSendInfo(string info, string infousername)
        {
            listBoxMessage.Items.Add(infousername + ": " + info);
        }
        public void ShowLogout(string userName, int userCount)
        {
            listBoxMessage.Items.Add(userName + "退出大厅。");
            userscount.Text = "在线人数：" + userCount.ToString();
            this.dataGrid.ItemsSource = null;
        }

        public void ShowWrongLogin(string username)
        {
            MessageBox.Show("用户" + username + "已经存在");
        }
        public void ShowWrongLogout(string username)
        {
            MessageBox.Show("用户" + username + "已经退出");
        }
        private void textBoxSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.btnSend_Click(sender, e);
            }
        }

        //显示在线用户
        public void ShowUserList(User[] users)
        {
            this.dataGrid.ItemsSource = null;
            this.dataGrid.ItemsSource = users;

        }
        //私聊
        public void btnPrivateChat_Click(object sender, RoutedEventArgs e)
        {
            var temp = dataGrid.SelectedItem;
            if (temp == null)
            {
                return;
            }
            if (textBoxSend.Text == String.Empty)
            {
                MessageBox.Show("发送的消息不能为空！");
                return;
            }
            User SelectUser = temp as User;
            try
            {
                string SelectName = SelectUser.userName;
                client.PrivateChat(UserName, SelectName, textBoxSend.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "私发出现错误");
            }

        }
        public void ShowPrivateChat(string userName, string text)
        {
            listBoxMessage.Items.Add(userName + "发给你： " + text);
            textBoxSend.Clear();
        }
    }
}
