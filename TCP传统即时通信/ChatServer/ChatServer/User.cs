using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    public class User
    {
        public BinaryReader br { get; private set; }
        public BinaryWriter bw { get; private set; }
        public string userName;
        private TcpClient client;

        public User(TcpClient client, bool isTask)
        {
            this.client = client;
            NetworkStream networkStream = client.GetStream();
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
            if (isTask)
            {
                Task.Run(() => ReceiveFromClient());
            }
            else
            {
                Thread t = new Thread(ReceiveFromClient);
                t.Start();
            }
        }

        public void Close()
        {
            br.Close();
            bw.Close();
            client.Close();
        }

        /// <summary>处理接收的客户端数据</summary>
        public void ReceiveFromClient()
        {
            while (true)
            {
                string receiveString = null;
                try
                {
                    receiveString = br.ReadString();
                }
                catch
                {
                    CC.RemoveUser(this); return;
                }
                string[] split = receiveString.Split(',');
                Console.WriteLine(receiveString + "???");
                switch (split[0])
                {
                    case "Login":  //格式：Login，用户名
                        userName = split[1];
                        CC.SendToAllClient("List" + CC.GetUserNames());
                        break;
                    case "Logout":  //格式：Logout,用户名
                        CC.RemoveUser(this);
                        CC.SendToAllClient(receiveString);
                        return;
                    case "Talk":  //格式：Talk，对话信息
                        CC.SendToAllClient(userName + "说：" + receiveString.Remove(0, 5));
                        break;
                    case "TalkToOne":
                        int temp = split[0].Length+2;
                        string msg = userName + "说：" + split[2];
                        CC.SendToOneClient(split[1],msg);
                        break;
                    default:
                        throw new Exception("无法解析：" + receiveString);
                }
            }
        }
    }
}
