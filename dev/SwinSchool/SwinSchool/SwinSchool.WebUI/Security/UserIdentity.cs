using SwinSchool.CommonShared.Dto;
using SwinSchool.WebUI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace SwinSchool.WebUI.Security
{
    public class UserIdentity : IIdentity
    {
        private string _userID;
        private string _email;
        private string _fullName;
        private string _tel;
        private string _address;

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
                return _fullName;
            }
        }
        #endregion

        #region Constructor
        // get user by user id
        public UserIdentity(MyUserDto user)
        {
            if (user == null) throw new Exception("User login failed");
            this._address = user.Address;
            this._email = user.Email;
            this._fullName = user.Name;
            this._tel = user.Tel;
            this._userID = user.UserID;

        }
        #endregion

        #region Public attributes
        public string UserID
        {
            get { return _userID; }
        }

        public string Email
        {
            get { return _email; }
        }

        public string Tel
        {
            get { return _tel; }
        }

        public string Address
        {
            get { return _address; }
        }
        #endregion
    }
}