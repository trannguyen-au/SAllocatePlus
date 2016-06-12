using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Tna.SAllocatePlus.CommonShared.Dto;

namespace Tna.SAllocatePlus.BusinessLogicServer
{
    [ServiceContract(Namespace = "http://Tna.SAllocatePlus.BusinessLogicServer")]
    public interface IJobService
    {
        [OperationContract]
        List<JobDto> GetJobsByCostCentre(string regionName);

        [OperationContract]
        List<JobDto> GetJobsByStaff(int staffId, bool hasAvailability);

        [OperationContract]
        void SetAvailabilityForJob(int bookId, int staffId, bool available);

        [OperationContract]
        List<JobStaffDto> GetStaffListForJob(int bookID);
        [OperationContract]
        void SendJobEmail(SendEmailRequestDto request);

        [OperationContract]
        JobDto GetJobById(int bookId);
        [OperationContract]
        List<JobAvailabilityDto> GetJobAvailabilityById(int bookId);
        [OperationContract]
        void UpdateJob(JobDto job);
    }
}
