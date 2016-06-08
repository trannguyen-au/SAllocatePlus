using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tna.SAllocatePlus.DataAccessLayer.Entities;

namespace Tna.SAllocatePlus.DataAccessLayer.Dao
{
    public interface IJobDao
    {
        List<Job> GetAll();
        List<Job> FindByRegion(CostCentre region);
        Job Get(int bookID);
        void Create(Job job);
        void Update(Job job);
        void Delete(Job job);
    }
}
