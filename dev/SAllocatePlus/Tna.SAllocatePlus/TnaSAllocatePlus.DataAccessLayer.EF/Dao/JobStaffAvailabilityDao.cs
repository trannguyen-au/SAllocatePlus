using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tna.SAllocatePlus.DataAccessLayer;
using Tna.SAllocatePlus.DataAccessLayer.EF;
using Tna.SAllocatePlus.DataAccessLayer.Entities;

namespace TnaSAllocatePlus.DataAccessLayer.EF.Dao
{
    public class JobStaffAvailabilityDao : IJobStaffAvailabilityDao
    {
        TnaContext _context;
        public JobStaffAvailabilityDao()
        {
            _context = new TnaContext();
        }
        public List<JobStaffAvailability> GetAllByStaff(int staffId, bool hasAvailability)
        {
            if (hasAvailability)
            {
                return _context.JobStaffAvailabilitySet.Where(a => a.StaffID == staffId && a.IsAvailable != null).ToList();
            }
            else
            {
                return _context.JobStaffAvailabilitySet.Where(a => a.StaffID == staffId && a.IsAvailable == null).ToList();
            }
        }


        public JobStaffAvailability Get(int bookId, int staffId)
        {
            return _context.JobStaffAvailabilitySet.FirstOrDefault(a => a.BookID == bookId && a.StaffID == staffId);
        }


        public void Update(JobStaffAvailability ja)
        {
            var entity = _context.JobStaffAvailabilitySet.FirstOrDefault(a => a.BookID == ja.BookID && a.StaffID == ja.StaffID);
            if (entity == null)
            {
                throw new Exception("JobStaffAvailability entity not found");
            }
            entity.IsAvailable = ja.IsAvailable;
            _context.SaveChanges();
        }


        public List<JobStaffAvailability> GetAllByJob(int bookId)
        {
            return _context.JobStaffAvailabilitySet.Where(a => a.BookID == bookId).ToList();
        }
    }
}
