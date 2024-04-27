namespace Insurance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class suaLaiDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "Image", c => c.String());
            AddColumn("dbo.Orders", "CustomerIdentity", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false));
            DropColumn("dbo.Products", "Alias");
            DropColumn("dbo.Products", "Image");
            DropColumn("dbo.Products", "MoreImages");
            DropColumn("dbo.Products", "Content");
            DropColumn("dbo.Products", "ViewCount");
            DropColumn("dbo.Orders", "DiscountPercent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "DiscountPercent", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "ViewCount", c => c.Int());
            AddColumn("dbo.Products", "Content", c => c.String());
            AddColumn("dbo.Products", "MoreImages", c => c.String(storeType: "xml"));
            AddColumn("dbo.Products", "Image", c => c.String(maxLength: 256));
            AddColumn("dbo.Products", "Alias", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Products", "Description", c => c.String());
            DropColumn("dbo.Orders", "CustomerIdentity");
            DropColumn("dbo.ProductCategories", "Image");
        }
    }
}
