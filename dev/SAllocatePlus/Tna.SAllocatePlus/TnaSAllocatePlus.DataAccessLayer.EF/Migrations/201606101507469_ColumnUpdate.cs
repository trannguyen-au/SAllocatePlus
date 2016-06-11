namespace TnaSAllocatePlus.DataAccessLayer.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobStaffAvailability", "IsAvailable", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobStaffAvailability", "IsAvailable", c => c.Boolean(nullable: false));
        }
    }
}
