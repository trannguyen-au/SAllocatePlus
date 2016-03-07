using SwinSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwinSchool.DAL.DAO
{
    public interface IMyUserDao
    {
        List<MyUser> GetAll();
        MyUser GetById(string userId);
        int Create(MyUser myUser);
        int Update(MyUser myUser);
        int Delete(MyUser myUser);
    }
}
