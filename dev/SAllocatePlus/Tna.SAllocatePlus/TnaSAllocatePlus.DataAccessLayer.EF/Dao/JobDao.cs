using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tna.SAllocatePlus.CommonShared;
using Tna.SAllocatePlus.DataAccessLayer.Dao;
using Tna.SAllocatePlus.DataAccessLayer.EF;
using Tna.SAllocatePlus.DataAccessLayer.Entities;

namespace TnaSAllocatePlus.DataAccessLayer.EF.Dao
{
    public class JobDao : IJobDao
    {
        TnaContext _context;
        public JobDao()
        {
            _context = new TnaContext();
        }
        public List<Job> GetAll()
        {
            return _context.JobSet.ToList();
        }

        public List<Job> FindByRegion(Region region)
        {
            return _context.JobSet.Where(j => j.JobRegion == region).ToList();
        }

        public Job Get(int bookID)
        {
            return _context.JobSet.FirstOrDefault(j => j.BookID == bookID);
        }

        public void Create(Job job)
        {
            _context.JobSet.Add(job);
            _context.SaveChanges();
        }

        public void Update(Job job)
        {
            var existingJob = _context.JobSet.FirstOrDefault(j => j.BookID == job.BookID);
            if (existingJob == null) throw new EntityNotFoundException();

            job.MergeTo(existingJob, "BookID", "JobStaffList");

            _context.SaveChanges();
        }

        public void Delete(Job job)
        {
            _context.JobSet.RemoveRange(_context.JobSet.Where(j => j.BookID == job.BookID));
        }
    }
}
