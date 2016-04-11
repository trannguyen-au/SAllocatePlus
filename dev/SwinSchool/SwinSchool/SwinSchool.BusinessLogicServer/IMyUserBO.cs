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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
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
        void ResetPassword(ResetPasswordRequestDto resetPasswordRequest);
    }
}
