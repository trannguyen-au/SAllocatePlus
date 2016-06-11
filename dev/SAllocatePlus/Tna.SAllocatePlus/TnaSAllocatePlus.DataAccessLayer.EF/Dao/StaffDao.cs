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


        public void Update(Staff myUser)
        {
            var entity = _context.StaffSet.FirstOrDefault(u => u.StaffID == myUser.StaffID);
            if (entity == null)
                throw new Exception("Staff does not existed");

            entity.FirstName = myUser.FirstName;
            entity.SurName = myUser.SurName;
            entity.Email = myUser.Email;
            entity.StaffCostCentre = myUser.StaffCostCentre;
            entity.Password = myUser.Password;
            entity.Active = myUser.Active;
            entity.Mobile = myUser.Mobile;
            entity.Username = myUser.Username;
            entity.CostCentre = _context.CostCentreSet.FirstOrDefault(cc=>cc.CostCentreCode == myUser.StaffCostCentre);

            _context.SaveChanges();
        }
    }
}
