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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Namespace = "http://Tna.SAllocatePlus.BusinessLogicServer")]
    public interface IJobService
    {

        [OperationContract]
        List<JobDto> GetJobsByCostCentre(string regionName);

        [OperationContract]
        List<JobStaffDto> GetStaffListForJob(int bookID);
    }
}
