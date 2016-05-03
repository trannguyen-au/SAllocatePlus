using SwinSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;

namespace SwinSchool.DAL
{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
            : base("SchoolContext")
        {}

        public DbSet<MyUser> MyUsers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
