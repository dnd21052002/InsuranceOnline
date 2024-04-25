namespace Insurance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProductCuss : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCustomers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(),
                        ExpireTime = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CustomerUsers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductCustomers", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductCustomers", "CustomerID", "dbo.CustomerUsers");
            DropIndex("dbo.ProductCustomers", new[] { "CustomerID" });
            DropIndex("dbo.ProductCustomers", new[] { "ProductID" });
            DropTable("dbo.ProductCustomers");
        }
    }
}
