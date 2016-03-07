using SwinSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace SwinSchool.DAL
{
    public class SchoolInitializer : DropCreateDatabaseIfModelChanges<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            var sampleUserList = new List<MyUser>();
            for (int i = 0; i < 10; i++)
            {
                sampleUserList.Add(
                    new MyUser
                    {
                        UserID = "10000"+i,
                        Name = "Alexander",
                        Email = "10000"+i+"@student.swin.edu.au",
                        Address = "1"+i+" Latrobe Street, Melbourne Vic 3000",
                        Tel = "04564353"+i.ToString("00"),
                        Password = "123456",
                        SecQn = "Favorite color?",
                        SecAns = "Green"
                    });
            }

            sampleUserList.ForEach(s => context.MyUsers.Add(s));
            context.SaveChanges();
        }
    }
}
