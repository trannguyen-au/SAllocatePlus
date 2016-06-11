namespace TnaSAllocatePlus.DataAccessLayer.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Job", "EmailSent", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Job", "EmailSent");
        }
    }
}
