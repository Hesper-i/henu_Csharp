using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proj1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LabelError.Visibility = Visibility.Collapsed;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUI();
        }

        private void ButtonScan_Click(object sender, RoutedEventArgs e)
        {
            int start;
            int end;

            if (int.TryParse(txtStart.Text, out start) && int.TryParse(txtEnd.Text, out end))
            {
                listBoxResult.Items.Clear();
                for (int i = start; i <= end; i++)
                {
                    string strip = txtPrefix.Text + i;
                    Thread t = new Thread(Scan);
                    t.Start(strip);
                }
            }
            else
            {
                ShowError();
            }
        }

        private void UpdateUI()
        {
            string strIP1 = txtPrefix.Text + txtStart.Text;
            string strIP2 = txtPrefix.Text + txtEnd.Text;
            IPAddress ip1, ip2;

            if (IPAddress.TryParse(strIP1, out ip1) && IPAddress.TryParse(strIP2, out ip2))
            {
                ButtonScan.IsEnabled = true;
                LabelError.Visibility = Visibility.Collapsed;
            }
            else
            {
                ShowError();
            }
        }

        private void ShowError()
        {
            LabelError.Visibility = Visibility.Visible;
            ButtonScan.IsEnabled = false;
        }

        private void Scan(object ip)
        {
            Stopwatch w = Stopwatch.StartNew();
            string host = "";

            try
            {
                host = Dns.GetHostEntry(ip.ToString()).HostName;
            }
            catch
            {
                host = "（不在线）";
            }

            w.Stop();

            this.listBoxResult.Dispatcher.Invoke(() =>
            {
                listBoxResult.Items.Add(string.Format("扫描地址：{0}，扫描用时：{1}毫秒，主机DNS名称：{2}",
                    ip, w.ElapsedMilliseconds, host));
            });
        }
    }

}
