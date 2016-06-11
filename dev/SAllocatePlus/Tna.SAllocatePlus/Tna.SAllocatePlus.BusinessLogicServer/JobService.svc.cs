using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Tna.SAllocatePlus.CommonShared.Dto;
using Tna.SAllocatePlus.CommonShared;
using Tna.SAllocatePlus.DataAccessLayer.Dao;
using Tna.SAllocatePlus.DataAccessLayer.Entities;
using TnaSAllocatePlus.DataAccessLayer.EF.Dao;
using Tna.SAllocatePlus.DataAccessLayer;


namespace Tna.SAllocatePlus.BusinessLogicServer
{
    public class JobService : IJobService
    {
        IJobDao _jobDao = null;
        IStaffDao _staffDao = null;
        ICommonDataDao _commonDataDao = null;
        IJobStaffAvailabilityDao _jobStaffAvailabilityDao = null;

        public JobService()
        {
            _jobDao = new JobDao();
            _staffDao = new StaffDao();
            _commonDataDao = new CommonDataDao();
            _jobStaffAvailabilityDao = new JobStaffAvailabilityDao();
        }
        public List<JobDto> GetJobsByCostCentre(string costCentre)
        {
            var queryResult = _jobDao.FindByRegion(new CostCentre()
            {
                CostCentreCode = costCentre
            });

            var result = new List<JobDto>();
            foreach (var job in queryResult)
            {
                var dto = new JobDto();
                job.MergeTo(dto, "JobStaffList", "JobSupervisor", "JobCostCentre");
                dto.JobCostCentre = job.CostCentre.CostCentreCode;
                if (job.JobSupervisor != null)
                {
                    dto.SupervisorName = string.Join(" ", job.Supervisor.FirstName, job.Supervisor.SurName);
                }

                result.Add(dto);
            }

            return result;
        }

        public List<JobStaffDto> GetStaffListForJob(int bookID)
        {
            var job = _jobDao.Get(bookID);
            if (job == null)
                throw new EntityNotFoundException();

            var staffList = new List<JobStaffDto>();
            foreach (var js in job.JobStaffList)
            {
                var dto = new JobStaffDto();
                js.MergeTo(dto);
                dto.BookID = js.Job.BookID;
                dto.StaffID = js.Staff.StaffID;

                staffList.Add(dto);
            }

            return staffList;
        }

        public void SendJobEmail(SendEmailRequestDto request)
        {
            var staffList = _staffDao.GetStaffsByIdList(request.StaffList).ToList();
            var costCentre = _commonDataDao.GetAllCostCentres().FirstOrDefault(cc=>cc.CostCentreCode == request.CostCentre);
            var fromEmail = costCentre.Email;

            EmailService emailService = new EmailService();
            bool isSentSuccess = true;
            foreach (var staff in staffList)
            {
                try
                {
                    emailService.SendMail(fromEmail, staff.Email, "Job schedule for " + request.CostCentre, request.Content.Replace("{StaffName}", string.Format("{0} {1}", staff.FirstName, staff.SurName)));

                }
                catch (Exception ex)
                {
                    isSentSuccess = false;
                    break;
                }
            }
            // update jobs
            if (isSentSuccess)
            {
                _jobDao.SetEmailSent(request.JobList);

                // create job staff data
                _jobDao.CreateJobStaffPreference(request.JobList, request.StaffList);
            }
        }


        public List<JobDto> GetJobsByStaff(int staffId, bool hasAvailability)
        {
            return Map(_jobStaffAvailabilityDao.GetAllByStaff(staffId, hasAvailability).Select(a => a.Job));
        }

        public void SetAvailabilityForJob(int bookId, int staffId, bool available)
        {
            var ja = _jobStaffAvailabilityDao.Get(bookId, staffId);
            if (ja == null)
                throw new Exception("Availability is not found");

            ja.IsAvailable = available;
            _jobStaffAvailabilityDao.Update(ja);
        }
        
        public JobDto GetJobById(int bookId)
        {
            return Map(_jobDao.Get(bookId));
        }

        public List<JobAvailabilityDto> GetJobAvailabilityById(int bookId)
        {
            var jobAvailability = _jobStaffAvailabilityDao.GetAllByJob(bookId);
            return Map(jobAvailability);
        }

        private List<JobDto> Map(IEnumerable<Job> enumerable)
        {
            return (from j in enumerable
                    select Map(j)).ToList();
        }

        private List<JobAvailabilityDto> Map(List<JobStaffAvailability> enumerable)
        {
            return (from j in enumerable
                    select Map(j)).ToList();
        }

        private JobAvailabilityDto Map(JobStaffAvailability j)
        {
            return new JobAvailabilityDto()
            {
                BookID = j.BookID,
                IsAvailable = j.IsAvailable,
                StaffID = j.StaffID,
                StaffName = string.Format("{0} {1}", j.Staff.FirstName, j.Staff.SurName)
            };
        }

        private JobDto Map(Job job)
        {
            var dto = new JobDto();
            job.MergeTo(dto, "JobStaffList", "JobSupervisor", "JobCostCentre");
            dto.JobCostCentre = job.CostCentre.CostCentreCode;
            if (job.JobSupervisor != null)
            {
                dto.SupervisorName = string.Join(" ", job.Supervisor.FirstName, job.Supervisor.SurName);
                dto.SupervisorStaffID = job.JobSupervisor.GetValueOrDefault();
            }
            return dto;
        }


        public void UpdateJob(JobDto job)
        {
            // business rule validation
            if(string.IsNullOrWhiteSpace(job.JobCostCentre)) 
                throw new Exception("Cost centre must not be empty");
            if(string.IsNullOrWhiteSpace(job.SiteName)) 
                throw new Exception("Site name must not be empty");

            // if valid, pass on to DAO to do update
            _jobDao.Update(job);
        }
    }
}
