using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Tna.SAllocatePlus.CommonShared.Dto;
using Tna.SAllocatePlus.DataAccessLayer;
using Tna.SAllocatePlus.DataAccessLayer.Entities;
using TnaSAllocatePlus.DataAccessLayer.EF.Dao;

namespace Tna.SAllocatePlus.BusinessLogicServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CommonDataService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CommonDataService.svc or CommonDataService.svc.cs at the Solution Explorer and start debugging.
    public class CommonDataService : ICommonDataService
    {
        ICommonDataDao _commonDataDao;

        public CommonDataService()
        {
            _commonDataDao = new CommonDataDao();
        }

        public List<RoleDto> GetAllRole()
        {
            return Map(_commonDataDao.GetAllRoles());
        }



        public List<CostCentreDto> GetAllCostCentre()
        {
            return Map(_commonDataDao.GetAllCostCentres());
        }

        #region Helper method
        private List<RoleDto> Map(List<Role> list)
        {
            return (from r in list
                    select new RoleDto()
                    {
                        RoleID = r.RoleID,
                        RoleName = r.RoleName
                    }).ToList();
        }

        private List<CostCentreDto> Map(List<CostCentre> list)
        {
            return (from r in list
                    select new CostCentreDto()
                    {
                        CostCentreCode= r.CostCentreCode,
                        Name = r.Name,
                        Email = r.Email,
                    }).ToList();
        }
        #endregion
    }
}
