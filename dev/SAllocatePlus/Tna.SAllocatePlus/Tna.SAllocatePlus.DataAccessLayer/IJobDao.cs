using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tna.SAllocatePlus.CommonShared.Dto;
using Tna.SAllocatePlus.DataAccessLayer.Entities;

namespace Tna.SAllocatePlus.DataAccessLayer.Dao
{
    public interface IJobDao
    {
        List<Job> GetAll();
        List<Job> FindByRegion(CostCentre region);
        Job Get(int bookID);
        void Create(JobDto job);
        void Update(JobDto job);
        void Delete(Job job);
        void SetEmailSent(List<int> list);

        void CreateJobStaffPreference(List<int> jobList, List<int> staffLIst);
    }
}
