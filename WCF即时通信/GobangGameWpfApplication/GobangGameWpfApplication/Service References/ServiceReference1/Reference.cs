﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace GobangGameWpfApplication.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/GobangGameWcfService")]
    [System.SerializableAttribute()]
    public partial class User : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string userNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string userName {
            get {
                return this.userNameField;
            }
            set {
                if ((object.ReferenceEquals(this.userNameField, value) != true)) {
                    this.userNameField = value;
                    this.RaisePropertyChanged("userName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="WcfGobangGameExample", ConfigurationName="ServiceReference1.IGobangService", CallbackContract=typeof(GobangGameWpfApplication.ServiceReference1.IGobangServiceCallback))]
    public interface IGobangService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfGobangGameExample/IGobangService/Login")]
        void Login(string userName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfGobangGameExample/IGobangService/Login")]
        System.Threading.Tasks.Task LoginAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfGobangGameExample/IGobangService/sendInfo")]
        void sendInfo(string info, string userName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfGobangGameExample/IGobangService/sendInfo")]
        System.Threading.Tasks.Task sendInfoAsync(string info, string userName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfGobangGameExample/IGobangService/Logout")]
        void Logout(string userName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfGobangGameExample/IGobangService/Logout")]
        System.Threading.Tasks.Task LogoutAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfGobangGameExample/IGobangService/LoginList")]
        void LoginList();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfGobangGameExample/IGobangService/LoginList")]
        System.Threading.Tasks.Task LoginListAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfGobangGameExample/IGobangService/PrivateChat")]
        void PrivateChat(string userName, string SelectName, string Text);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfGobangGameExample/IGobangService/PrivateChat")]
        System.Threading.Tasks.Task PrivateChatAsync(string userName, string SelectName, string Text);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGobangServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfGobangGameExample/IGobangService/ShowLogin")]
        void ShowLogin(string userName, int userCount);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfGobangGameExample/IGobangService/ShowSendInfo")]
        void ShowSendInfo(string info, string infousername);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfGobangGameExample/IGobangService/ShowLogout")]
        void ShowLogout(string userName, int userCount);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfGobangGameExample/IGobangService/ShowWrongLogin")]
        void ShowWrongLogin(string userName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfGobangGameExample/IGobangService/ShowWrongLogout")]
        void ShowWrongLogout(string userName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfGobangGameExample/IGobangService/ShowUserList")]
        void ShowUserList(GobangGameWpfApplication.ServiceReference1.User[] users);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfGobangGameExample/IGobangService/ShowPrivateChat")]
        void ShowPrivateChat(string userName, string text);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGobangServiceChannel : GobangGameWpfApplication.ServiceReference1.IGobangService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GobangServiceClient : System.ServiceModel.DuplexClientBase<GobangGameWpfApplication.ServiceReference1.IGobangService>, GobangGameWpfApplication.ServiceReference1.IGobangService {
        
        public GobangServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public GobangServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public GobangServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GobangServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GobangServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Login(string userName) {
            base.Channel.Login(userName);
        }
        
        public System.Threading.Tasks.Task LoginAsync(string userName) {
            return base.Channel.LoginAsync(userName);
        }
        
        public void sendInfo(string info, string userName) {
            base.Channel.sendInfo(info, userName);
        }
        
        public System.Threading.Tasks.Task sendInfoAsync(string info, string userName) {
            return base.Channel.sendInfoAsync(info, userName);
        }
        
        public void Logout(string userName) {
            base.Channel.Logout(userName);
        }
        
        public System.Threading.Tasks.Task LogoutAsync(string userName) {
            return base.Channel.LogoutAsync(userName);
        }
        
        public void LoginList() {
            base.Channel.LoginList();
        }
        
        public System.Threading.Tasks.Task LoginListAsync() {
            return base.Channel.LoginListAsync();
        }
        
        public void PrivateChat(string userName, string SelectName, string Text) {
            base.Channel.PrivateChat(userName, SelectName, Text);
        }
        
        public System.Threading.Tasks.Task PrivateChatAsync(string userName, string SelectName, string Text) {
            return base.Channel.PrivateChatAsync(userName, SelectName, Text);
        }
    }
}
