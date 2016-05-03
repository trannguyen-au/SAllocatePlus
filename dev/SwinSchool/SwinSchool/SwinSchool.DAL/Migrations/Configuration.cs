namespace SwinSchool.DAL.Migrations
{
    using SwinSchool.DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SwinSchool.DAL.SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SwinSchool.DAL.SchoolContext context)
        {
            var sampleUserList = new List<MyUser>();
            for (int i = 0; i < 10; i++)
            {
                sampleUserList.Add(
                    new MyUser
                    {
                        UserID = "10000" + i,
                        Name = "Alexander",
                        Email = "10000" + i + "@student.swin.edu.au",
                        Address = "1" + i + " Latrobe Street, Melbourne Vic 3000",
                        Tel = "04564353" + i.ToString("00"),
                        Password = "123456",
                        SecQn = "Favorite color?",
                        SecAns = "Green"
                    });
            }

            sampleUserList.ForEach(s => context.MyUsers.Add(s));
            context.SaveChanges();

            var sampleProductList = new List<Product>();

            for (var i = 1; i < 10; i++)
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
