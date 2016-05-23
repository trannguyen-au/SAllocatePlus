using SwinSchool.CommonShared.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SwinSchool.WebUI.Service
{


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IMyUserBO", SessionMode = System.ServiceModel.SessionMode.Required)]
    public interface IMyUserBO
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMyUserBO/GetAllUsers", ReplyAction = "http://tempuri.org/IMyUserBO/GetAllUsersResponse")]
        SwinSchool.CommonShared.Dto.MyUserDto[] GetAllUsers();

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMyUserBO/CreateUser", ReplyAction = "http://tempuri.org/IMyUserBO/CreateUserResponse")]
        void CreateUser(SwinSchool.CommonShared.Dto.MyUserDto userDto);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMyUserBO/UpdateUser", ReplyAction = "http://tempuri.org/IMyUserBO/UpdateUserResponse")]
        void UpdateUser(SwinSchool.CommonShared.Dto.MyUserDto userDto);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMyUserBO/GetUserById", ReplyAction = "http://tempuri.org/IMyUserBO/GetUserByIdResponse")]
        SwinSchool.CommonShared.Dto.MyUserDto GetUserById(string id);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMyUserBO/DeleteUser", ReplyAction = "http://tempuri.org/IMyUserBO/DeleteUserResponse")]
        void DeleteUser(string userId);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMyUserBO/ResetPassword", ReplyAction = "http://tempuri.org/IMyUserBO/ResetPasswordResponse")]
        string[] ResetPassword(SwinSchool.CommonShared.Dto.ResetPasswordRequestDto resetPasswordRequest);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMyUserBO/PrecheckForResetPassword", ReplyAction = "http://tempuri.org/IMyUserBO/PrecheckForResetPasswordResponse")]
        string[] PrecheckForResetPassword(SwinSchool.CommonShared.Dto.ResetPasswordRequestDto resetPasswordRequest);
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
    }

}