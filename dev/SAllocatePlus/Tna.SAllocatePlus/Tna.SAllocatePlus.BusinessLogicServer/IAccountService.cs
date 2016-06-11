using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Tna.SAllocatePlus.CommonShared.Dto;

namespace Tna.SAllocatePlus.BusinessLogicServer
{
    [ServiceContract(Namespace = "http://Tna.SAllocatePlus.BusinessLogicServer")]
    public interface IAccountService
    {
        [OperationContract]
        void UpdateStaff(StaffAccountDto staffData);
        [OperationContract]
        List<string> ResetPassword(ResetPasswordRequestDto resetPasswordRequest);
        [OperationContract]
        List<string> PrecheckForResetPassword(ResetPasswordRequestDto resetPasswordRequest);

        [OperationContract]
        StaffAccountDto ValidateLogin(string username, byte[] password);

        [OperationContract]
        StaffAccountDto GetUserById(int id);

        [OperationContract]
        List<StaffAccountDto> GetStaffsByCostCentre(string costCentre);
    }
}
