using SwinSchool.CommonShared;
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
                        SecAns = "Green",
                        Role = Constants.RoleValue.Employee
                    });
            }
            sampleUserList.Add(
                    new MyUser
                    {
                        UserID = "234561",
                        Name = "Wery Nguyen",
                        Email = "nguyennt86@gmail.com",
                        Address = "32 Baker St, Marylebone, London W1U 3EY",
                        Tel = "0456435356",
                        Password = "123456",
                        SecQn = "Favorite color?",
                        SecAns = "Red",
                        Role = Constants.RoleValue.Administrator
                    });

            sampleUserList.ForEach(s => context.MyUsers.Add(s));
            context.SaveChanges();

            var sampleProductList = new List<Product>();

            for (var i =1; i < 10; i++)
            {
                sampleProductList.Add(new Product()
                {
                    ProductID = "P0000" + i,
                    Name = "Item number " + i,
                    Quantity = 100,
                    SellingPrice = 15.90m + i * 2,
                    Cost = 10 + i
                });
            }
            sampleProductList.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
        }
    }
}
