using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.Entity;
using SwinSchool.CommonShared.Dto;
using SwinSchool.DAL.DAO;
using SwinSchool.DAL.DAO.Impl;
using SwinSchool.DAL.Entities;
using SwinSchool.CommonShared;

namespace SwinSchool.BusinessLogicServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class MyUserBO : IMyUserBO
    {
        private IMyUserDao _myUserDao;
        private UserMaintenanceService _userService;

        public MyUserBO()
        {
            _myUserDao = new MyUserDao();

            // initialize user service to listen to the MSMQ
            _userService = UserMaintenanceService.Instance;
        }

        public void CreateUser(MyUserDto userDto)
        {
            var myUser = Mapping.Map<MyUser, MyUserDto>(userDto);
            _myUserDao.Create(myUser);
        }

        public void DeleteUser(string userId)
        {
            var myUser = new MyUser()
            {
                UserID = userId
            };
            _myUserDao.Delete(myUser);
        }

        public void DeleteUser(MyUserDto userDto)
        {
            var myUser = Mapping.Map<MyUser, MyUserDto>(userDto);
            _myUserDao.Delete(myUser);
        }

        public List<MyUserDto> GetAllUsers()
        {
            var allUser = _myUserDao.GetAll();
            var mappingResult = Mapping.Map<IEnumerable<MyUserDto>, IEnumerable<MyUser>>(allUser);
            return mappingResult.ToList();
        }

        public MyUserDto GetUserById(string id)
        {
            var user = _myUserDao.GetById(id);
            if (user != null)
            {
                var mappingResult = Mapping.Map<MyUserDto, MyUser>(user);
                return mappingResult;
            }
            return null;
        }

        /// <summary>
        /// PT3.2 Implementation
        /// </summary>
        /// <param name="resetPasswordRequest"></param>
        /// <returns></returns>
        public List<string> ResetPassword(ResetPasswordRequestDto resetPasswordRequest)
        {
            List<string> errors = PrecheckForResetPassword(resetPasswordRequest);
            
            return errors;
        }

        public void UpdateUser(MyUserDto userDto)
        {
            var myUser = Mapping.Map<MyUser, MyUserDto>(userDto);
            _myUserDao.Update(myUser);
        }


        public List<string> PrecheckForResetPassword(ResetPasswordRequestDto resetPasswordRequest)
        {
            List<string> errors = new List<string>();
            // Reset password validation:
            var user = _myUserDao.GetById(resetPasswordRequest.UserId);
            if (user == null)
                errors.Add("User is not found");

            if (!user.SecAns.Equals(resetPasswordRequest.SecAns))
                errors.Add("Your security answer is invalid");

            return errors;
        }
    }
}
