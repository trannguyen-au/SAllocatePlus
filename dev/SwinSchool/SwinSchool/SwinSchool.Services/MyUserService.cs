using SwinSchool.CommonShared.Dto;
using SwinSchool.DAL.DAO;
using SwinSchool.DAL.DAO.AdoImpl;
using SwinSchool.DAL.DAO.Impl;
using SwinSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwinSchool.Services
{
    public class MyUserService : ServiceBase
    {
        private IMyUserDao _myUserDao;

        public MyUserService()
        {
            _myUserDao = new MyUserDao();
        }

        public MyUserService(string connectionString)
        {
            _myUserDao = new MyUserAdoDao(connectionString);
        }

        public List<MyUserDto> GetAllUsers()
        {
            var allUser = _myUserDao.GetAll();
            var mappingResult = Mapping.Map<IEnumerable<MyUserDto>, IEnumerable<MyUser>>(allUser);
            return mappingResult.ToList();
        }

        public void CreateUser(MyUserDto userDto)
        {
            var myUser = Mapping.Map<MyUser, MyUserDto>(userDto);
            _myUserDao.Create(myUser);
        }

        public void UpdateUser(MyUserDto userDto)
        {
            var myUser = Mapping.Map<MyUser, MyUserDto>(userDto);
            _myUserDao.Update(myUser);
        }

        public MyUserDto GetUserById(string id)
        {
            var user = _myUserDao.GetById(id);
            if(user!=null)
            {
                var mappingResult = Mapping.Map<MyUserDto, MyUser>(user);
                return mappingResult;
            }
            return null;
            
        }

        public void DeleteUser(MyUserDto userDto)
        {
            var myUser = Mapping.Map<MyUser, MyUserDto>(userDto);
            _myUserDao.Delete(myUser);
        }

        public void DeleteUser(string userId)
        {
            var myUser = new MyUser()
            {
                UserID = userId
            };
            _myUserDao.Delete(myUser);
        }
    }
}
