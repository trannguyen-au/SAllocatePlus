using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tna.SAllocatePlus.DataAccessLayer.Entities;

namespace Tna.SAllocatePlus.DataAccessLayer
{
    public interface ICommonDataDao
    {
        List<CostCentre> GetAllCostCentres();
        List<Role> GetAllRoles();
    }
}
