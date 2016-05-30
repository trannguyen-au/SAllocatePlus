﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5485
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SwinSchool.CommonShared.Dto
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MyUserDto", Namespace="http://schemas.datacontract.org/2004/07/SwinSchool.CommonShared.Dto")]
    public partial class MyUserDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string AddressField;
        
        private string EmailField;
        
        private string NameField;
        
        private string RoleField;
        
        private string TelField;
        
        private string UserIDField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address
        {
            get
            {
                return this.AddressField;
            }
            set
            {
                this.AddressField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email
        {
            get
            {
                return this.EmailField;
            }
            set
            {
                this.EmailField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Role
        {
            get
            {
                return this.RoleField;
            }
            set
            {
                this.RoleField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Tel
        {
            get
            {
                return this.TelField;
            }
            set
            {
                this.TelField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserID
        {
            get
            {
                return this.UserIDField;
            }
            set
            {
                this.UserIDField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ResetPasswordRequestDto", Namespace="http://schemas.datacontract.org/2004/07/SwinSchool.CommonShared.Dto")]
    public partial class ResetPasswordRequestDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string SecAnsField;
        
        private string UserIdField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SecAns
        {
            get
            {
                return this.SecAnsField;
            }
            set
            {
                this.SecAnsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserId
        {
            get
            {
                return this.UserIdField;
            }
            set
            {
                this.UserIdField = value;
            }
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IMyUserBO", SessionMode=System.ServiceModel.SessionMode.Required)]
public interface IMyUserBO
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMyUserBO/GetAllUsers", ReplyAction="http://tempuri.org/IMyUserBO/GetAllUsersResponse")]
    SwinSchool.CommonShared.Dto.MyUserDto[] GetAllUsers();
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMyUserBO/CreateUser", ReplyAction="http://tempuri.org/IMyUserBO/CreateUserResponse")]
    void CreateUser(SwinSchool.CommonShared.Dto.MyUserDto userDto);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMyUserBO/UpdateUser", ReplyAction="http://tempuri.org/IMyUserBO/UpdateUserResponse")]
    void UpdateUser(SwinSchool.CommonShared.Dto.MyUserDto userDto);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMyUserBO/GetUserById", ReplyAction="http://tempuri.org/IMyUserBO/GetUserByIdResponse")]
    SwinSchool.CommonShared.Dto.MyUserDto GetUserById(string id);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMyUserBO/DeleteUser", ReplyAction="http://tempuri.org/IMyUserBO/DeleteUserResponse")]
    void DeleteUser(string userId);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMyUserBO/ResetPassword", ReplyAction="http://tempuri.org/IMyUserBO/ResetPasswordResponse")]
    string[] ResetPassword(SwinSchool.CommonShared.Dto.ResetPasswordRequestDto resetPasswordRequest);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMyUserBO/PrecheckForResetPassword", ReplyAction="http://tempuri.org/IMyUserBO/PrecheckForResetPasswordResponse")]
    string[] PrecheckForResetPassword(SwinSchool.CommonShared.Dto.ResetPasswordRequestDto resetPasswordRequest);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMyUserBO/ValidateLogin", ReplyAction="http://tempuri.org/IMyUserBO/ValidateLoginResponse")]
    SwinSchool.CommonShared.Dto.MyUserDto ValidateLogin(string username, string password);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
public interface IMyUserBOChannel : IMyUserBO, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
public partial class MyUserBOClient : System.ServiceModel.ClientBase<IMyUserBO>, IMyUserBO
{
    
    public MyUserBOClient()
    {
    }
    
    public MyUserBOClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public MyUserBOClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public MyUserBOClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public MyUserBOClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public SwinSchool.CommonShared.Dto.MyUserDto[] GetAllUsers()
    {
        return base.Channel.GetAllUsers();
    }
    
    public void CreateUser(SwinSchool.CommonShared.Dto.MyUserDto userDto)
    {
        base.Channel.CreateUser(userDto);
    }
    
    public void UpdateUser(SwinSchool.CommonShared.Dto.MyUserDto userDto)
    {
        base.Channel.UpdateUser(userDto);
    }
    
    public SwinSchool.CommonShared.Dto.MyUserDto GetUserById(string id)
    {
        return base.Channel.GetUserById(id);
    }
    
    public void DeleteUser(string userId)
    {
        base.Channel.DeleteUser(userId);
    }
    
    public string[] ResetPassword(SwinSchool.CommonShared.Dto.ResetPasswordRequestDto resetPasswordRequest)
    {
        return base.Channel.ResetPassword(resetPasswordRequest);
    }
    
    public string[] PrecheckForResetPassword(SwinSchool.CommonShared.Dto.ResetPasswordRequestDto resetPasswordRequest)
    {
        return base.Channel.PrecheckForResetPassword(resetPasswordRequest);
    }
    
    public SwinSchool.CommonShared.Dto.MyUserDto ValidateLogin(string username, string password)
    {
        return base.Channel.ValidateLogin(username, password);
    }
}
