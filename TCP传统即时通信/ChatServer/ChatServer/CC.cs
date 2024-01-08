using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChatServer
{
    public class CC
    {
        public static List<User> userList { get; set; }  //保存连接的所有用户
        public static IPAddress localIP { get; set; }  //使用的本机IPv4地址
        public static int port { get; set; }   //监听端口

        static CC()
        {
            userList = new List<User>();
            port = 53985;
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var v in ips)
            {
                if (v.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = v; break;
                }
            }
        }

        public static string GetUserNames()
        {
            string result="";
            foreach(User item in userList)
            {

                result += ","+ item.userName ;
            }
            return result;
        }

        public static void RemoveUser(User user)
        {
            userList.Remove(user);
            SendToAllClient("有用户退出或失去连接，当前用户数：" + userList.Count);
        }

        public static void SendToAllClient(string message)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                try
                {
                    userList[i].bw.Write(message);
                    userList[i].bw.Flush();
                }
                catch
                {
                    RemoveUser(userList[i]);
                }
            }
        }

        public static void SendToOneClient(string toUserName,string message)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i].userName == toUserName)
                {
                    try
                    {
                        userList[i].bw.Write(message);
                        Console.WriteLine(message+"!!!");
                        userList[i].bw.Flush();
                    }
                    catch
                    {
                        RemoveUser(userList[i]);
                    }
                    break;
                }
            }
        }

        public static void ChangeState(Button btn1, bool b1, Button btn2, bool b2)
        {
            btn1.IsEnabled = b1;
            btn2.IsEnabled = b2;
        }
    }
}
