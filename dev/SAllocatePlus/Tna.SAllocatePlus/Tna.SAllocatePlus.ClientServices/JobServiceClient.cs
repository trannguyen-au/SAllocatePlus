﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5485
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tna.SAllocatePlus.ClientServices
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://Tna.SAllocatePlus.BusinessLogicServer", ConfigurationName = "IJobService")]
    public interface IJobService
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://Tna.SAllocatePlus.BusinessLogicServer/IJobService/GetJobsByCostCentre", ReplyAction = "http://Tna.SAllocatePlus.BusinessLogicServer/IJobService/GetJobsByCostCentreRespo" +
            "nse")]
        Tna.SAllocatePlus.CommonShared.Dto.JobDto[] GetJobsByCostCentre(string regionName);

        [System.ServiceModel.OperationContractAttribute(Action = "http://Tna.SAllocatePlus.BusinessLogicServer/IJobService/GetStaffListForJob", ReplyAction = "http://Tna.SAllocatePlus.BusinessLogicServer/IJobService/GetStaffListForJobRespon" +
            "se")]
        Tna.SAllocatePlus.CommonShared.Dto.JobStaffDto[] GetStaffListForJob(int bookID);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IJobServiceChannel : IJobService, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class JobServiceClient : System.ServiceModel.ClientBase<IJobService>, IJobService
    {

        public JobServiceClient()
        {
        }

        public JobServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public JobServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public JobServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public JobServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public Tna.SAllocatePlus.CommonShared.Dto.JobDto[] GetJobsByCostCentre(string regionName)
        {
            return base.Channel.GetJobsByCostCentre(regionName);
        }

        public Tna.SAllocatePlus.CommonShared.Dto.JobStaffDto[] GetStaffListForJob(int bookID)
        {
            return base.Channel.GetStaffListForJob(bookID);
        }
    }

}
