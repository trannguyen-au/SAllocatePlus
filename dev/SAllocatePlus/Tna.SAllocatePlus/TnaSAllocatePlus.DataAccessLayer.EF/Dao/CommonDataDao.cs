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
    public class CommonDataDao : ICommonDataDao
    {
        private TnaContext _context;

        public CommonDataDao()
        {
            _context = new TnaContext();
        }

        public List<CostCentre> GetAllCostCentres()
        {
            return _context.CostCentreSet.ToList();
        }

        public List<Role> GetAllRoles()
        {
            return _context.RoleSet.ToList();
        }
    }
}
