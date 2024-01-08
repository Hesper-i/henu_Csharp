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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            ThreadClientWindow w1 = new ThreadClientWindow()
            {
                Title = "客户端1-多线程接收",
                Left = this.Left - 130,
                Top = this.Top + 70,
            };
            w1.textBoxUserName.Text = "User1"; w1.Owner = this; w1.Show();
            ThreadClientWindow w2 = new ThreadClientWindow()
            {
                Title = "客户端2-多线程接收",
                Left = this.Left + 260,
                Top = this.Top + 70,
            };
            w2.textBoxUserName.Text = "User2"; w2.Owner = this; w2.Show();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            TaskClientWindow w1 = new TaskClientWindow()
            {
                Title = "客户端1-多任务接收",
                Left = this.Left - 130,
                Top = this.Top + 70,
            };
            w1.textBoxUserName.Text = "User1"; w1.Owner = this; w1.Show();
            TaskClientWindow w2 = new TaskClientWindow()
            {
                Title = "客户端2-多任务接收",
                Left = this.Left + 260,
                Top = this.Top + 70,
            };
            w2.textBoxUserName.Text = "User2"; w2.Owner = this; w2.Show();
        }
    }
}
