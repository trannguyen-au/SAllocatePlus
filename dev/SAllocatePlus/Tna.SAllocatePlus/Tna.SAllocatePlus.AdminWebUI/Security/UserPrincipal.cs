using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Tna.SAllocatePlus.ClientServices;
using Tna.SAllocatePlus.CommonShared;
using Tna.SAllocatePlus.CommonShared.Dto;

namespace Tna.SAllocatePlus.AdminWebUI.Security
{
    public class UserPrincipal : IPrincipal, IIdentity
    {
        private List<string> _roleList;
        AccountServiceClient _clientProxy = ServiceFactory.CreateAccountServiceClient();

        StaffAccountDto _serializedData;

        public IIdentity Identity
        {
            get
            {
                return this;
            }
        }

        public List<string> ReadAccessCostCentre
        {
            get
            {
                return _serializedData.AccessList
                    .Where(c => c.AccessRights == AccessRightsEnum.Read 
                        || c.AccessRights == AccessRightsEnum.Write)
                    .Select(c => c.CostCentreCode).ToList();
            }
        }

        public List<string> WriteAccessCostCentre
        {
            get
            {
                return _serializedData.AccessList
                    .Where(c => c.AccessRights == AccessRightsEnum.Write)
                    .Select(c => c.CostCentreCode).ToList();
            }
        }

        public bool IsInRole(string role)
        {
            if (role.Contains("|"))
            {
                var checkRoleList = role.Split("|".ToCharArray());
                foreach (var roleItem in checkRoleList)
                {
                    if (_roleList.Contains(roleItem)) return true;
                }
                return false;
            }
            else if (role.Contains("&"))
            {
                var checkRoleList = role.Split("&".ToCharArray());
                foreach (var roleItem in checkRoleList)
                {
                    if (!_roleList.Contains(roleItem)) return false;
                }
                return true;
            }
            return _roleList.Contains(role);
        }

        public StaffAccountDto SerializedData
        {
            get
            {
                return _serializedData;
            }
        }

        public UserPrincipal(StaffAccountDto userData)
        {
            _serializedData = userData;
            _roleList = userData.RoleList;
        }

        public UserPrincipal(int userId)
        {
            var userData = _clientProxy.GetUserById(userId);
            _serializedData = userData;
        }

        public static UserPrincipal ValidateLogin(string userId, string password)
        {
            var cyperPass = EncryptionService.EncryptPassword(password);
            var clientProxy = ServiceFactory.CreateAccountServiceClient();

            var userData = clientProxy.ValidateLogin(userId, cyperPass);

            if (userData == null)
            {
                throw new Exception("Your username and password is invalid");
            }
            return new UserPrincipal(userData);
        }



        #region IIdentity methods
        public string AuthenticationType
        {
            get
            {
                return "Custom Authentication";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get
            {
                return string.Format("{0} {1}", _serializedData.FirstName, _serializedData.SurName);
            }
        }
        #endregion
    }
}