namespace SwinSchool.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyUser",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 10),
                        Name = c.String(maxLength: 30),
                        Password = c.String(maxLength: 12),
                        Email = c.String(maxLength: 100),
                        Tel = c.String(maxLength: 10),
                        Address = c.String(maxLength: 60),
                        SecQn = c.String(maxLength: 60),
                        SecAns = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.String(nullable: false, maxLength: 6),
                        Name = c.String(maxLength: 30),
                        Quantity = c.Int(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Product");
            DropTable("dbo.MyUser");
        }
    }
}
