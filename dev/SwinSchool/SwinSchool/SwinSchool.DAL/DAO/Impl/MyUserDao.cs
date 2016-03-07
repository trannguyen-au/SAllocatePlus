using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwinSchool.DAL.DAO
{
    public class MyUserDao : IMyUserDao
    {
        private SchoolContext _context;
        public MyUserDao()
        {
            _context = new SchoolContext();
        }
        public List<Entities.MyUser> GetAll()
        {
            return _context.MyUsers.ToList();
        }

        public Entities.MyUser GetById(string userId)
        {
            return _context.MyUsers.FirstOrDefault(u => u.UserID == userId);
        }

        public int Create(Entities.MyUser myUser)
        {
            if (_context.MyUsers.Any(u => u.UserID == myUser.UserID))
                throw new Exception("User existed");
            
            _context.MyUsers.Add(myUser);
            return _context.SaveChanges();
        }

        public int Update(Entities.MyUser myUser)
        {
            var updatingUser = _context.MyUsers.FirstOrDefault(u => u.UserID == myUser.UserID);
            if (updatingUser==null)
                throw new Exception("User does not existed");

            updatingUser.Address = myUser.Address;
            updatingUser.Email = myUser.Email;
            updatingUser.Name = myUser.Name;
            updatingUser.Password = myUser.Password;
            updatingUser.SecAns = myUser.SecAns;
            updatingUser.SecQn = myUser.SecQn;
            updatingUser.Tel = myUser.Tel;

            return _context.SaveChanges();
        }

        public int Delete(Entities.MyUser myUser)
        {
            var deletingUser = _context.MyUsers.FirstOrDefault(u => u.UserID == myUser.UserID);
            if (deletingUser != null)
            {
                _context.MyUsers.Remove(deletingUser);
                return _context.SaveChanges();
            }
            return 0;
        }
    }
}
