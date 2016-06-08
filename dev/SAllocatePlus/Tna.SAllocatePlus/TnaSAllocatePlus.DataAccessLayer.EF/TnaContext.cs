using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tna.SAllocatePlus.DataAccessLayer.Entities;

namespace Tna.SAllocatePlus.DataAccessLayer.EF
{
    public class TnaContext : DbContext
    {
        public TnaContext() : base("TnaContext") { }

        public DbSet<CostCentre> CostCentreSet { get; set; }
        public DbSet<Job> JobSet { get; set; }
        public DbSet<JobStaff> JobStaffSet { get; set; }
        public DbSet<JobStaffAvailability> JobStaffAvailabilitySet { get; set; }
        public DbSet<Role> RoleSet { get; set; }
        public DbSet<Staff> StaffSet{ get; set; }
        public DbSet<StaffAccessRight> StaffAccessRightSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
