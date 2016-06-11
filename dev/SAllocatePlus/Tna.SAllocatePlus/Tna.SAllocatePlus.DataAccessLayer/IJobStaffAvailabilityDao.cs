using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tna.SAllocatePlus.DataAccessLayer.Entities;

namespace Tna.SAllocatePlus.DataAccessLayer
{
    public interface IJobStaffAvailabilityDao
    {
        List<JobStaffAvailability> GetAllByStaff(int staffId, bool hasAvailability);

        List<JobStaffAvailability> GetAllByJob(int bookId);

        JobStaffAvailability Get(int bookId, int staffId);

        void Update(JobStaffAvailability ja);

        
    }
}
