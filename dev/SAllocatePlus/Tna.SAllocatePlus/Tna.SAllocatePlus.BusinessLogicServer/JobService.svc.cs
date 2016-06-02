﻿using System;
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


namespace Tna.SAllocatePlus.BusinessLogicServer
{
    public class JobService : IJobService
    {
        IJobDao _jobDao = null;

        public JobService()
        {
            _jobDao = new JobDao();
        }
        public List<JobDto> GetJobsByRegion(string regionId)
        {
            var queryResult = _jobDao.FindByRegion(new Region()
            {
                RegionID = regionId
            });

            var result = new List<JobDto>();
            foreach (var job in queryResult)
            {
                var dto = new JobDto();
                job.MergeTo(dto, "JobStaffList", "JobSupervisor", "JobRegion");
                if (job.JobSupervisor != null)
                {
                    dto.SupervisorName = string.Join(" ", job.JobSupervisor.FirstName, job.JobSupervisor.SurName);
                    dto.JobRegion = job.JobRegion.RegionID;
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
    }
}