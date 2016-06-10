using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Tna.SAllocatePlus.CommonShared.Dto;
using Tna.SAllocatePlus.DataAccessLayer;
using Tna.SAllocatePlus.DataAccessLayer.EF;
using Tna.SAllocatePlus.DataAccessLayer.Entities;

namespace TnaSAllocatePlus.DataAccessLayer.EF.Dao
{
    public class StaffDao : IStaffDao
    {
        TnaContext _context;

        public StaffDao()
        {
            _context = new TnaContext();
        }

        public void CreateStaff(Staff staffAccount)
        {
            
        }

        public Staff GetStaffByID(int staffID)
        {
            return _context.StaffSet
                .Include(s => s.RoleList)
                .Include(s => s.AccessList)
                .FirstOrDefault(s => s.StaffID == staffID);
        }


        public Staff GetStaffByUserName(string userName)
        {
            return _context.StaffSet
                .Include(s => s.RoleList)
                .Include(s => s.AccessList)
                .FirstOrDefault(s => s.Username == userName);
        }


        public List<Staff> GetStaffByCostCentre(string costCentre)
        {
            return _context.StaffSet.Where(s => s.StaffCostCentre == costCentre && s.Active).ToList();
        }


        public List<Staff> GetStaffsByIdList(List<int> list)
        {
            return _context.StaffSet.Where(s => list.Contains(s.StaffID)).ToList();
        }
    }
}
