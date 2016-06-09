using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tna.SAllocatePlus.CommonShared.Dto;
using Tna.SAllocatePlus.DataAccessLayer.Entities;

namespace Tna.SAllocatePlus.DataAccessLayer
{
    public interface IStaffDao
    {
        void CreateStaff(Staff staffAccount);
        Staff GetStaffByID(int staffID);
        Staff GetStaffByUserName(string userName);

        List<Staff> GetStaffByCostCentre(string costCentre);
    }
}
