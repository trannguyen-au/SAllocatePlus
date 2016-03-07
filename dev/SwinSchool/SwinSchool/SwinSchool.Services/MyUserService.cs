using SwinSchool.CommonShared.Dto;
using SwinSchool.DAL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwinSchool.Services
{
    public class MyUserService
    {
        private IMyUserDao _myUserDao;

        public MyUserService()
        {
            _myUserDao = new MyUserDao();
        }

        public List<MyUserDto> GetAllUsers()
        {
            var allUser = _myUserDao.GetAll();
            var mappingResult = Mapping.Map<IEnumerable<MyUserDto>>(allUser);
            return mappingResult.ToList();
        }
    }
}
