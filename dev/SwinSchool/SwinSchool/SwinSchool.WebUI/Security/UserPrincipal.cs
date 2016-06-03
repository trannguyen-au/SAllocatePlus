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

namespace SwinSchool.WebUI.Security
{
    public class UserPrincipal : IPrincipal
    {
        private UserIdentity _identity;
        private List<string> _roleList;
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
            if(role.Contains("|"))
            {
                var checkRoleList = role.Split("|".ToCharArray());
                foreach(var roleItem in checkRoleList)
                {
                    if (_roleList.Contains(roleItem)) return true;
                }
                return false;
            }
            else if(role.Contains("&"))
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
            _roleList = _identity.Role.Split(",".ToCharArray()).ToList();
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
            return new UserPrincipal(userData);
        }

        
    }
}