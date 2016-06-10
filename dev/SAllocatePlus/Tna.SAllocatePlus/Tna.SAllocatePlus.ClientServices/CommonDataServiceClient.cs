using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tna.SAllocatePlus.ClientServices
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://Tna.SAllocatePlus.BusinessLogicServer", ConfigurationName = "ICommonDataService")]
    public interface ICommonDataService
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://Tna.SAllocatePlus.BusinessLogicServer/ICommonDataService/GetAllRole", ReplyAction = "http://Tna.SAllocatePlus.BusinessLogicServer/ICommonDataService/GetAllRoleRespons" +
            "e")]
        Tna.SAllocatePlus.CommonShared.Dto.RoleDto[] GetAllRole();

        [System.ServiceModel.OperationContractAttribute(Action = "http://Tna.SAllocatePlus.BusinessLogicServer/ICommonDataService/GetAllCostCentre", ReplyAction = "http://Tna.SAllocatePlus.BusinessLogicServer/ICommonDataService/GetAllCostCentreR" +
            "esponse")]
        Tna.SAllocatePlus.CommonShared.Dto.CostCentreDto[] GetAllCostCentre();
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface ICommonDataServiceChannel : ICommonDataService, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class CommonDataServiceClient : System.ServiceModel.ClientBase<ICommonDataService>, ICommonDataService
    {

        public CommonDataServiceClient()
        {
        }

        public CommonDataServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public CommonDataServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public CommonDataServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public CommonDataServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public Tna.SAllocatePlus.CommonShared.Dto.RoleDto[] GetAllRole()
        {
            return base.Channel.GetAllRole();
        }

        public Tna.SAllocatePlus.CommonShared.Dto.CostCentreDto[] GetAllCostCentre()
        {
            return base.Channel.GetAllCostCentre();
        }
    }

}
