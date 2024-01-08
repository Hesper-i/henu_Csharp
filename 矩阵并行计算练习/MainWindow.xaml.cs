using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Diagnostics;

namespace Problem4
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Stopwatch stopwatch = new Stopwatch();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            long[] t1 = await Task.Run(() => Multiply(300, 24, 36));
            textblock.Text = string.Format("测试1(矩阵1：300×24，矩阵2：24×36），非并行用时：{0}毫秒，并行用时：{1}毫秒", t1[0], t1[1]);
            long[] t2 = await Task.Run(() => Multiply(3000, 240, 360));
            textblock.Text += string.Format("\n测试2(矩阵2：3000×240，矩阵2：240×360），非并行用时：{0}毫秒，并行用时：{1}毫秒", t2[0], t2[1]);
            long[] t3 = await Task.Run(() => Multiply(3000, 500, 300));
            textblock.Text += string.Format("\n测试3(矩阵3：3000×500，矩阵2：500×300），非并行用时：{0}毫秒，并行用时：{1}毫秒", t3[0], t3[1]);
        }

        private long[] Multiply(int m, int n, int p)
        {
            long[] timeElapsed = new long[2];
            double[,] m1 = InitMatrix(m, n);
            double[,] m2 = InitMatrix(n, p);
            double[,] result = new double[m, p];
            //串行
            stopwatch.Restart();
            result = new double[m, p];
            Compute(m1, m2, result);
            stopwatch.Stop();
            timeElapsed[0] = stopwatch.ElapsedMilliseconds;

            //并行
            stopwatch.Restart();
            result = new double[m, p];
            ParallelCompute(m1, m2, result);
            stopwatch.Stop();
            timeElapsed[1] = stopwatch.ElapsedMilliseconds;

            return timeElapsed;
        }
        //串行算法
        private void Compute(double[,] a, double[,] b, double[,] result)
        {
            int aRows = a.GetLength(0);
            int aCols = a.GetLength(1);
            int bCols = b.GetLength(1);
            for (int i = 0; i < aRows; i++)
            {
                for (int j = 0; j < bCols; j++)
                {
                    double temp = 0;
                    for (int k = 0; k < aCols; k++)
                    {
                        temp += a[i, k] * b[k, j];
                    }
                    result[i, j] = temp;
                }
            }
        }
        //并行算法
        private void ParallelCompute(double[,] a, double[,] b, double[,] result)
        {
            int aRows = a.GetLength(0);
            int aCols = a.GetLength(1);
            int bCols = b.GetLength(1);

            Parallel.For(0, aRows, (i) =>   
            {
                for (int j = 0; j < bCols; j++)
                {
                    double temp = 0;  
                    for (int k = 0; k < aCols; k++)
                    {
                        temp += a[i, k] * b[k, j];
                    }
                    result[i, j] = temp;
                }
            });
        }
        //初始化矩阵
        private double[,] InitMatrix(int row, int col) 
        {
            double[,] data = new double[row, col];
            Random r = new Random();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    data[i, j] = r.Next(100);
                }
            }
            return data;
        }
    }
}
