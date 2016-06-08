namespace TnaSAllocatePlus.DataAccessLayer.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CostCentre",
                c => new
                    {
                        CostCentreCode = c.String(nullable: false, maxLength: 20),
                        Name = c.String(maxLength: 50),
                        Email = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.CostCentreCode);
            
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
                        JobCostCentre = c.String(maxLength: 20),
                        JobSupervisor = c.Int(),
                    })
                .PrimaryKey(t => t.BookID)
                .ForeignKey("dbo.CostCentre", t => t.JobCostCentre)
                .ForeignKey("dbo.Staff", t => t.JobSupervisor)
                .Index(t => t.JobCostCentre)
                .Index(t => t.JobSupervisor);
            
            CreateTable(
                "dbo.JobStaff",
                c => new
                    {
                        JobStaffID = c.Int(nullable: false, identity: true),
                        JSStaffID = c.Int(nullable: false),
                        JSBookID = c.Int(nullable: false),
                        StartTime = c.Time(precision: 7),
                        StartDate = c.DateTime(),
                        IsSupervisor = c.Boolean(nullable: false),
                        IsStaffConfirmed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.JobStaffID)
                .ForeignKey("dbo.Job", t => t.JSBookID, cascadeDelete: false)
                .ForeignKey("dbo.Staff", t => t.JSStaffID, cascadeDelete: false)
                .Index(t => t.JSStaffID)
                .Index(t => t.JSBookID);
            
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
                        StaffCostCentre = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.StaffID)
                .ForeignKey("dbo.CostCentre", t => t.StaffCostCentre, cascadeDelete: false)
                .Index(t => t.StaffCostCentre);
            
            CreateTable(
                "dbo.StaffAccessRight",
                c => new
                    {
                        AccessRightID = c.Int(nullable: false, identity: true),
                        StaffUserID = c.Int(nullable: false),
                        CostCentreCode = c.String(nullable: false, maxLength: 20),
                        AccessRights = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccessRightID)
                .ForeignKey("dbo.CostCentre", t => t.CostCentreCode, cascadeDelete: false)
                .ForeignKey("dbo.Staff", t => t.StaffUserID, cascadeDelete: false)
                .Index(t => t.StaffUserID)
                .Index(t => t.CostCentreCode);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.RoleID);
            
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
                "dbo.RoleStaff",
                c => new
                    {
                        Role_RoleID = c.Int(nullable: false),
                        Staff_StaffID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_RoleID, t.Staff_StaffID })
                .ForeignKey("dbo.Role", t => t.Role_RoleID, cascadeDelete: true)
                .ForeignKey("dbo.Staff", t => t.Staff_StaffID, cascadeDelete: true)
                .Index(t => t.Role_RoleID)
                .Index(t => t.Staff_StaffID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobStaffAvailability", "StaffID", "dbo.Staff");
            DropForeignKey("dbo.JobStaffAvailability", "BookID", "dbo.Job");
            DropForeignKey("dbo.Job", "JobSupervisor", "dbo.Staff");
            DropForeignKey("dbo.JobStaff", "JSStaffID", "dbo.Staff");
            DropForeignKey("dbo.RoleStaff", "Staff_StaffID", "dbo.Staff");
            DropForeignKey("dbo.RoleStaff", "Role_RoleID", "dbo.Role");
            DropForeignKey("dbo.Staff", "StaffCostCentre", "dbo.CostCentre");
            DropForeignKey("dbo.StaffAccessRight", "StaffUserID", "dbo.Staff");
            DropForeignKey("dbo.StaffAccessRight", "CostCentreCode", "dbo.CostCentre");
            DropForeignKey("dbo.JobStaff", "JSBookID", "dbo.Job");
            DropForeignKey("dbo.Job", "JobCostCentre", "dbo.CostCentre");
            DropIndex("dbo.RoleStaff", new[] { "Staff_StaffID" });
            DropIndex("dbo.RoleStaff", new[] { "Role_RoleID" });
            DropIndex("dbo.JobStaffAvailability", new[] { "StaffID" });
            DropIndex("dbo.JobStaffAvailability", new[] { "BookID" });
            DropIndex("dbo.StaffAccessRight", new[] { "CostCentreCode" });
            DropIndex("dbo.StaffAccessRight", new[] { "StaffUserID" });
            DropIndex("dbo.Staff", new[] { "StaffCostCentre" });
            DropIndex("dbo.JobStaff", new[] { "JSBookID" });
            DropIndex("dbo.JobStaff", new[] { "JSStaffID" });
            DropIndex("dbo.Job", new[] { "JobSupervisor" });
            DropIndex("dbo.Job", new[] { "JobCostCentre" });
            DropTable("dbo.RoleStaff");
            DropTable("dbo.JobStaffAvailability");
            DropTable("dbo.Role");
            DropTable("dbo.StaffAccessRight");
            DropTable("dbo.Staff");
            DropTable("dbo.JobStaff");
            DropTable("dbo.Job");
            DropTable("dbo.CostCentre");
        }
    }
}
