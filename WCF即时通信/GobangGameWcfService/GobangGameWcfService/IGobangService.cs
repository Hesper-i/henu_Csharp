using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using static GobangGameWcfService.GobangService;


namespace GobangGameWcfService
{
    //需要服务端实现的协定
    [ServiceContract(Namespace = "WcfGobangGameExample",
        CallbackContract = typeof(IGobangServiceCallback))]
    public interface IGobangService
    {
        [OperationContract(IsOneWay = true)]
        void Login(string userName);
        [OperationContract(IsOneWay = true)]
        void sendInfo(string info, string userName);
        [OperationContract(IsOneWay = true)]
        void Logout(string userName);

        [OperationContract(IsOneWay = true)]
        void LoginList();

        [OperationContract(IsOneWay = true)]
        void PrivateChat(string userName, string SelectName, string Text);
    }
    [DataContract]
    public class users
    {
        [DataMember] public String userName { get; set; }
    }
    [DataContract]
    public class User
    {
        /// <summary>登录的用户名</summary>
        [DataMember] public string userName;
        public DateTime loginTime;
        public readonly IGobangServiceCallback callback;
        public User(string userName, IGobangServiceCallback callback)
        {
            //this.userName = userName;
            this.callback = callback;
        }
    }

    //需要客户端实现的接口
    public interface IGobangServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void ShowLogin(string userName, int userCount);
        [OperationContract(IsOneWay = true)]
        void ShowSendInfo(string info, string infousername);
        [OperationContract(IsOneWay = true)]
        void ShowLogout(string userName, int userCount);
        [OperationContract(IsOneWay = true)]
        void ShowWrongLogin(string userName);

        [OperationContract(IsOneWay = true)]
        void ShowWrongLogout(string userName);

        [OperationContract(IsOneWay = true)]
        void ShowUserList(List<User> users);

        [OperationContract(IsOneWay = true)]
        void ShowPrivateChat(string userName, string text);

    }

}
