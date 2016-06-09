using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tna.SAllocatePlus.ClientServices
{


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://Tna.SAllocatePlus.BusinessLogicServer", ConfigurationName = "IAccountService")]
    public interface IAccountService
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://Tna.SAllocatePlus.BusinessLogicServer/IAccountService/ResetPassword", ReplyAction = "http://Tna.SAllocatePlus.BusinessLogicServer/IAccountService/ResetPasswordRespons" +
            "e")]
        string[] ResetPassword(Tna.SAllocatePlus.CommonShared.Dto.ResetPasswordRequestDto resetPasswordRequest);

        [System.ServiceModel.OperationContractAttribute(Action = "http://Tna.SAllocatePlus.BusinessLogicServer/IAccountService/PrecheckForResetPass" +
            "word", ReplyAction = "http://Tna.SAllocatePlus.BusinessLogicServer/IAccountService/PrecheckForResetPass" +
            "wordResponse")]
        string[] PrecheckForResetPassword(Tna.SAllocatePlus.CommonShared.Dto.ResetPasswordRequestDto resetPasswordRequest);

        [System.ServiceModel.OperationContractAttribute(Action = "http://Tna.SAllocatePlus.BusinessLogicServer/IAccountService/ValidateLogin", ReplyAction = "http://Tna.SAllocatePlus.BusinessLogicServer/IAccountService/ValidateLoginRespons" +
            "e")]
        Tna.SAllocatePlus.CommonShared.Dto.StaffAccountDto ValidateLogin(string username, byte[] password);

        [System.ServiceModel.OperationContractAttribute(Action = "http://Tna.SAllocatePlus.BusinessLogicServer/IAccountService/GetUserById", ReplyAction = "http://Tna.SAllocatePlus.BusinessLogicServer/IAccountService/GetUserByIdResponse")]
        Tna.SAllocatePlus.CommonShared.Dto.StaffAccountDto GetUserById(int id);

        [System.ServiceModel.OperationContractAttribute(Action = "http://Tna.SAllocatePlus.BusinessLogicServer/IAccountService/GetStaffsByCostCentr" +
            "e", ReplyAction = "http://Tna.SAllocatePlus.BusinessLogicServer/IAccountService/GetStaffsByCostCentr" +
            "eResponse")]
        Tna.SAllocatePlus.CommonShared.Dto.StaffAccountDto[] GetStaffsByCostCentre(string costCentre);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IAccountServiceChannel : IAccountService, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class AccountServiceClient : System.ServiceModel.ClientBase<IAccountService>, IAccountService
    {

        public AccountServiceClient()
        {
        }

        public AccountServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public AccountServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public AccountServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public AccountServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public string[] ResetPassword(Tna.SAllocatePlus.CommonShared.Dto.ResetPasswordRequestDto resetPasswordRequest)
        {
            return base.Channel.ResetPassword(resetPasswordRequest);
        }

        public string[] PrecheckForResetPassword(Tna.SAllocatePlus.CommonShared.Dto.ResetPasswordRequestDto resetPasswordRequest)
        {
            return base.Channel.PrecheckForResetPassword(resetPasswordRequest);
        }

        public Tna.SAllocatePlus.CommonShared.Dto.StaffAccountDto ValidateLogin(string username, byte[] password)
        {
            return base.Channel.ValidateLogin(username, password);
        }

        public Tna.SAllocatePlus.CommonShared.Dto.StaffAccountDto GetUserById(int id)
        {
            return base.Channel.GetUserById(id);
        }

        public Tna.SAllocatePlus.CommonShared.Dto.StaffAccountDto[] GetStaffsByCostCentre(string costCentre)
        {
            return base.Channel.GetStaffsByCostCentre(costCentre);
        }
    }

}
