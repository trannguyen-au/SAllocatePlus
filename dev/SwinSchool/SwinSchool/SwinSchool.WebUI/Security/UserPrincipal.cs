using SwinSchool.CommonShared;
using SwinSchool.CommonShared.Dto;
using SwinSchool.WebUI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Security;

namespace SwinSchool.WebUI.Security
{
    public class UserPrincipal : IPrincipal
    {
        private UserIdentity _identity;
        MyUserBOClient _clientProxy = ServiceFactory.CreateUserBoClient();

        MyUserDto _serializedData;

        public IIdentity Identity
        {
            get
            {
                return _identity;
            }
        }

        public bool IsInRole(string role)
        {
            return Roles.IsUserInRole(_identity.UserID, role);
        }

        public MyUserDto SerializedData
        {
            get
            {
                return _serializedData;
            }
        }

        public UserPrincipal(MyUserDto userData)
        {
            _serializedData = userData;
            _identity = new UserIdentity(userData);
        }

        public UserPrincipal(string userId)
        {
            var userData = _clientProxy.GetUserById(userId);
            _identity = new UserIdentity(userData);
        }

        public static UserPrincipal ValidateLogin(string userId, string password)
        {
            var cyperPass = EncryptionService.EncryptPassword(password);
            var clientProxy = ServiceFactory.CreateUserBoClient();

            var userData = clientProxy.ValidateLogin(userId, cyperPass);

            if(userData == null)
            {
                throw new Exception("Your username and password is invalid");
            }

            // a quick integration method to copy current role data from enterprise database to 
            // client sql server database. This will make sure that the user role data is stored on the RoleDatabase
            // so that the SqlRoleProvider works accordingly.
            var userRoleData = Roles.GetRolesForUser(userData.UserID);
            if (userRoleData== null || userRoleData.Length == 0)
            {
                Roles.AddUserToRole(userData.UserID, userData.Role);
            }
            return new UserPrincipal(userData);
        }

        
    }
}