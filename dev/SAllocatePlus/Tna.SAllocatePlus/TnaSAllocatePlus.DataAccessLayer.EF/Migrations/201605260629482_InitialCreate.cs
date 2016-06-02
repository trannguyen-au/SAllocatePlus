namespace TnaSAllocatePlus.DataAccessLayer.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        BookID = c.Int(nullable: false, identity: true),
                        SiteName = c.String(nullable: false),
                        SiteAddress = c.String(nullable: false),
                        JobStage = c.Int(nullable: false),
                        JobDate = c.DateTime(),
                        JobTime = c.Time(precision: 7),
                        StaffRequired = c.Int(nullable: false),
                        JobDetails = c.String(),
                        JobRegion_RegionID = c.String(maxLength: 20),
                        JobSupervisor_StaffID = c.Int(),
                    })
                .PrimaryKey(t => t.BookID)
                .ForeignKey("dbo.Region", t => t.JobRegion_RegionID)
                .ForeignKey("dbo.Staff", t => t.JobSupervisor_StaffID)
                .Index(t => t.JobRegion_RegionID)
                .Index(t => t.JobSupervisor_StaffID);
            
            CreateTable(
                "dbo.Region",
                c => new
                    {
                        RegionID = c.String(nullable: false, maxLength: 20),
                        RegionName = c.String(),
                    })
                .PrimaryKey(t => t.RegionID);
            
            CreateTable(
                "dbo.JobStaff",
                c => new
                    {
                        JobStaffID = c.Int(nullable: false, identity: true),
                        StartTime = c.Time(precision: 7),
                        StartDate = c.DateTime(),
                        IsSupervisor = c.Boolean(nullable: false),
                        IsStaffConfirmed = c.Boolean(nullable: false),
                        Job_BookID = c.Int(nullable: false),
                        Staff_StaffID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobStaffID)
                .ForeignKey("dbo.Job", t => t.Job_BookID, cascadeDelete: true)
                .ForeignKey("dbo.Staff", t => t.Staff_StaffID, cascadeDelete: true)
                .Index(t => t.Job_BookID)
                .Index(t => t.Staff_StaffID);
            
            CreateTable(
                "dbo.Staff",
                c => new
                    {
                        StaffID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        SurName = c.String(maxLength: 100),
                        Mobile = c.String(maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 100),
                        Username = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        Active = c.Boolean(nullable: false),
                        StaffRegion_RegionID = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.StaffID)
                .ForeignKey("dbo.Region", t => t.StaffRegion_RegionID, cascadeDelete: true)
                .Index(t => t.StaffRegion_RegionID);
            
            CreateTable(
                "dbo.JobStaffAvailability",
                c => new
                    {
                        BookID = c.Int(nullable: false),
                        StaffID = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookID, t.StaffID })
                .ForeignKey("dbo.Job", t => t.BookID, cascadeDelete: true)
                .ForeignKey("dbo.Staff", t => t.StaffID, cascadeDelete: true)
                .Index(t => t.BookID)
                .Index(t => t.StaffID);
            
            CreateTable(
                "dbo.StaffUser",
                c => new
                    {
                        StaffUserID = c.Guid(nullable: false),
                        Username = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        StaffName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.StaffUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobStaffAvailability", "StaffID", "dbo.Staff");
            DropForeignKey("dbo.JobStaffAvailability", "BookID", "dbo.Job");
            DropForeignKey("dbo.Job", "JobSupervisor_StaffID", "dbo.Staff");
            DropForeignKey("dbo.JobStaff", "Staff_StaffID", "dbo.Staff");
            DropForeignKey("dbo.Staff", "StaffRegion_RegionID", "dbo.Region");
            DropForeignKey("dbo.JobStaff", "Job_BookID", "dbo.Job");
            DropForeignKey("dbo.Job", "JobRegion_RegionID", "dbo.Region");
            DropIndex("dbo.JobStaffAvailability", new[] { "StaffID" });
            DropIndex("dbo.JobStaffAvailability", new[] { "BookID" });
            DropIndex("dbo.Staff", new[] { "StaffRegion_RegionID" });
            DropIndex("dbo.JobStaff", new[] { "Staff_StaffID" });
            DropIndex("dbo.JobStaff", new[] { "Job_BookID" });
            DropIndex("dbo.Job", new[] { "JobSupervisor_StaffID" });
            DropIndex("dbo.Job", new[] { "JobRegion_RegionID" });
            DropTable("dbo.StaffUser");
            DropTable("dbo.JobStaffAvailability");
            DropTable("dbo.Staff");
            DropTable("dbo.JobStaff");
            DropTable("dbo.Region");
            DropTable("dbo.Job");
        }
    }
}
