using SwinSchool.CommonShared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SwinSchool.BusinessLogicServer
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IMyUserBO
    {
        [OperationContract]
        List<MyUserDto> GetAllUsers();
        [OperationContract]
        void CreateUser(MyUserDto userDto);
        [OperationContract]
        void UpdateUser(MyUserDto userDto);
        [OperationContract]
        MyUserDto GetUserById(string id);
        [OperationContract]
        void DeleteUser(string userId);

        [OperationContract]
        List<string> ResetPassword(ResetPasswordRequestDto resetPasswordRequest);
        [OperationContract]
        List<string> PrecheckForResetPassword(ResetPasswordRequestDto resetPasswordRequest);

        [OperationContract]
        MyUserDto ValidateLogin(string username, string password);
    }
}
