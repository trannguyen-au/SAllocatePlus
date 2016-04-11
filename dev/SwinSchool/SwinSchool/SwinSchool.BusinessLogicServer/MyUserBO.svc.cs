using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SwinSchool.CommonShared.Dto;
using SwinSchool.DAL.DAO;
using SwinSchool.DAL.DAO.Impl;
using SwinSchool.DAL.Entities;
using SwinSchool.CommonShared;

namespace SwinSchool.BusinessLogicServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class MyUserBO : IMyUserBO
    {
        private IMyUserDao _myUserDao;

        public MyUserBO()
        {
            _myUserDao = new MyUserDao();
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

        public void ResetPassword(ResetPasswordRequestDto resetPasswordRequest)
        {
            // Reset password validation:
            var user = _myUserDao.GetById(resetPasswordRequest.UserId);
            if (user == null)
                throw new Exception("User is not found");

            if (!user.SecAns.Equals(resetPasswordRequest.SecAns))
                throw new Exception("Your security answer is invalid");

            // Perform generating new password.
            user.Password = RandomManager.GenerateRandomString(8);

            // save updated entity to the database
            if (_myUserDao.Update(user) > 0)
            {
                // log the updated password into a server log file
                AppLogger.Info(string.Format("Password has been updated for user : {0} to : {1}", user.Name, user.Password));
            }
        }

        public void UpdateUser(MyUserDto userDto)
        {
            var myUser = Mapping.Map<MyUser, MyUserDto>(userDto);
            _myUserDao.Update(myUser);
        }
    }
}
