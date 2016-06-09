using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Tna.SAllocatePlus.CommonShared;
using Tna.SAllocatePlus.CommonShared.Dto;
using Tna.SAllocatePlus.DataAccessLayer;
using Tna.SAllocatePlus.DataAccessLayer.Entities;
using TnaSAllocatePlus.DataAccessLayer.EF.Dao;

namespace Tna.SAllocatePlus.BusinessLogicServer
{
    public class AccountService : IAccountService
    {
        IStaffDao _staffDao;

        public AccountService()
        {
            _staffDao = new StaffDao();
        }

        public List<string> ResetPassword(CommonShared.Dto.ResetPasswordRequestDto resetPasswordRequest)
        {
            throw new NotImplementedException();
        }

        public List<string> PrecheckForResetPassword(CommonShared.Dto.ResetPasswordRequestDto resetPasswordRequest)
        {
            throw new NotImplementedException();
        }

        public StaffAccountDto GetUserById(int id)
        {
            var user = _staffDao.GetStaffByID(id);
            if (user != null)
            {
                return Map(user);
            }
            return null;
        }

        public StaffAccountDto ValidateLogin(string username, byte[] encryptedPassword)
        {
            var user= _staffDao.GetStaffByUserName(username);

            // check if user is found
            if (user == null)
                return null;

            // check if user is active
            if (!user.Active)
                return null;

            // check for valid password
            var userPassword = EncryptionService.EncryptPassword(user.Password);
            if (Encoding.UTF8.GetString(encryptedPassword) != Encoding.UTF8.GetString(userPassword))
            {
                return null;
            }

            return Map(user);
        }


        public List<StaffAccountDto> GetStaffsByCostCentre(string costCentre)
        {
            var staffList = _staffDao.GetStaffByCostCentre(costCentre);
            return (from s in staffList
                    select MapNoDependency(s)).ToList();
        }



        #region Mapping methods
        private StaffAccountDto Map(Staff user)
        {
            var dto = new StaffAccountDto()
            {
                StaffID = user.StaffID,
                Active = user.Active,
                Email = user.Email,
                FirstName = user.FirstName,
                SurName = user.SurName,
                Mobile = user.Mobile,
                StaffCostCentre = user.StaffCostCentre,
                Username = user.Username
            };

            dto.RoleList = (from r in user.RoleList
                            select r.RoleName).ToList();
            dto.AccessList = (from a in user.AccessList
                              select new AccessRightsDto()
                              {
                                  AccessRights = a.AccessRights,
                                  CostCentreCode = a.CostCentreCode
                              }).ToList();
            return dto;
        }

        private StaffAccountDto MapNoDependency(Staff user)
        {
            var dto = new StaffAccountDto()
            {
                StaffID = user.StaffID,
                Active = user.Active,
                Email = user.Email,
                FirstName = user.FirstName,
                SurName = user.SurName,
                Mobile = user.Mobile,
                StaffCostCentre = user.StaffCostCentre,
                Username = user.Username
            };

            return dto;
        }
        #endregion
    }
}
