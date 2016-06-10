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
    public interface ICommonDataService
    {
        [OperationContract]
        List<RoleDto> GetAllRole();
        [OperationContract]
        List<CostCentreDto> GetAllCostCentre();
    }
}
