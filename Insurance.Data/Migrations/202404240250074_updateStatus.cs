namespace Insurance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateStatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Status", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ProductCategories", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductCategories", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "Status", c => c.Int(nullable: false));
        }
    }
}
