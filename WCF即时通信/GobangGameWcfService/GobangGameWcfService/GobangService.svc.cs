using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace GobangGameWcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    public class GobangService : IGobangService
    {
        public GobangService()
        {
            if (Users.userList == null)
                Users.userList = new List<User>();
        }

        private IGobangServiceCallback callback;

        public void Login(string userName)
        {
            OperationContext context = OperationContext.Current;
            callback = context.GetCallbackChannel<IGobangServiceCallback>();
            User newUser = new User(userName, callback);
            newUser.userName = userName;
            foreach (var userone in Users.userList)
            {
                //新增
                if (newUser.userName == userone.userName)
                {
                    callback.ShowWrongLogin(userone.userName);
                    return;
                }
            }

            Users.userList.Add(newUser);

            foreach (var usertwo in Users.userList)
            {
                usertwo.callback.ShowLogin(userName, Users.userList.Count);
                //usertwo.callback.ShowUserList(Users.userList);
            }

        }
        public void sendInfo(string info, string userName)
        {
            foreach (var user in Users.userList)
            {
                user.callback.ShowSendInfo(info, userName);
            }
        }

        //私聊功能
        public void PrivateChat(string userName, string SelectName, string T)
        {
            foreach (User user in Users.userList)
            {
                if (user.userName == SelectName)
                {
                    user.callback.ShowPrivateChat(userName, T);
                }
            }
        }

        public void Logout(string userName)
        {
            User removeUser = null;
            foreach (User one in Users.userList)
            {
                if (one.userName == userName)
                {
                    removeUser = one;
                    break;
                }
            }
            //新增
            if (removeUser == null)
            {
                callback.ShowWrongLogout(userName);
                return;
            }

            Users.userList.Remove(removeUser);

            foreach (var user in Users.userList)
            {
                user.callback.ShowLogout(userName, Users.userList.Count);
                
                //列表！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
                user.callback.ShowUserList(Users.userList);
            }
        }

        public void LoginList()
        {
            foreach (User user in Users.userList)
            {
                user.callback.ShowUserList(Users.userList);
            }

        }
        public class Users
        {
            public static List<User> userList;
        }


    }
}
