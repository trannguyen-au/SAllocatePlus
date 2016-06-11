using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tna.SAllocatePlus.CommonShared;
using Tna.SAllocatePlus.CommonShared.Dto;
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

        public List<Job> FindByRegion(CostCentre costCentre)
        {
            return _context.JobSet.Where(j => j.JobCostCentre == costCentre.CostCentreCode).ToList();
        }

        public Job Get(int bookID)
        {
            return _context.JobSet.FirstOrDefault(j => j.BookID == bookID);
        }

        public void Create(JobDto dto)
        {
            var job = new Job()
            {
                CostCentre = _context.CostCentreSet.FirstOrDefault(cc => cc.CostCentreCode == dto.JobCostCentre),
                EmailSent = dto.EmailSent,
                JobDate = dto.JobDate,
                JobDetails = dto.JobDetails,
                JobStage = dto.JobStage,
                JobSupervisor = dto.SupervisorStaffID,
                JobTime = dto.JobTime,
                SiteAddress = dto.SiteAddress,
                SiteName = dto.SiteName,
                StaffRequired = dto.StaffRequired,
                Supervisor = _context.StaffSet.FirstOrDefault(s => s.StaffID == dto.SupervisorStaffID)
            };

            _context.JobSet.Add(job);
            _context.SaveChanges();
        }

        public void Update(JobDto dto)
        {
            var existingJob = _context.JobSet.FirstOrDefault(j => j.BookID == dto.BookID);
            if (existingJob == null) throw new EntityNotFoundException();

            dto.MergeTo(existingJob, "BookID", "JobStaffList", "CostCentre", "Supervisor");

            existingJob.CostCentre = _context.CostCentreSet.FirstOrDefault(cc => cc.CostCentreCode == dto.JobCostCentre);
            existingJob.Supervisor = _context.StaffSet.FirstOrDefault(s => s.StaffID == dto.SupervisorStaffID);

            _context.SaveChanges();
        }

        public void Delete(Job job)
        {
            _context.JobSet.RemoveRange(_context.JobSet.Where(j => j.BookID == job.BookID));
        }


        public void SetEmailSent(List<int> list)
        {
            var jobList = _context.JobSet.Where(j => list.Contains(j.BookID));
            foreach (var j in jobList)
            {
                j.EmailSent = true;
            }
            _context.SaveChanges();
        }


        public void CreateJobStaffPreference(List<int> jobList, List<int> staffList)
        {
            var jobData = _context.JobSet.Where(j => jobList.Contains(j.BookID)).ToList();
            var staffData = _context.StaffSet.Where(s => staffList.Contains(s.StaffID)).ToList();
            var existingJobAvailability = _context.JobStaffAvailabilitySet.Where(a => jobList.Contains(a.BookID) || staffList.Contains(a.StaffID)).ToList();

            foreach(var staff in staffData)
                foreach (var job in jobData)
                {
                    // skip existing
                    if (existingJobAvailability.Any(a => a.StaffID == staff.StaffID && a.BookID == job.BookID)) continue;

                    var jobStaff = new JobStaffAvailability()
                    {
                        Job = job,
                        Staff= staff,
                        IsAvailable = null
                    };
                    _context.JobStaffAvailabilitySet.Add(jobStaff);
                }

            _context.SaveChanges();
        }
    }
}
