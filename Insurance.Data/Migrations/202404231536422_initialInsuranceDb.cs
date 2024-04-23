namespace Insurance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialInsuranceDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        FullName = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CartID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Carts", t => t.CartID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.CartID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerUserId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerUsers", t => t.CustomerUserId, cascadeDelete: true)
                .Index(t => t.CustomerUserId);
            
            CreateTable(
                "dbo.CustomerUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        FullName = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        BirthDay = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Alias = c.String(nullable: false, maxLength: 256),
                        CategoryID = c.Int(nullable: false),
                        Image = c.String(maxLength: 256),
                        MoreImages = c.String(storeType: "xml"),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PromotionPrice = c.Decimal(precision: 18, scale: 2),
                        Warranty = c.Int(),
                        Description = c.String(),
                        Content = c.String(),
                        HomeFlag = c.Boolean(),
                        HotFlag = c.Boolean(),
                        ViewCount = c.Int(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductCategories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        DisplayOrder = c.Int(),
                        HomeFlag = c.Boolean(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.OrderID, t.ProductID })
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 256),
                        CustomerAddress = c.String(nullable: false, maxLength: 256),
                        CustomerEmail = c.String(nullable: false, maxLength: 256),
                        CustomerMobile = c.String(nullable: false, maxLength: 20),
                        CustomerMessage = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentMethod = c.String(),
                        PaymentStatus = c.String(nullable: false),
                        DiscountPercent = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CustomerUsers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.CustomerUsers");
            DropForeignKey("dbo.CartItems", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.ProductCategories");
            DropForeignKey("dbo.CartItems", "CartID", "dbo.Carts");
            DropForeignKey("dbo.Carts", "CustomerUserId", "dbo.CustomerUsers");
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            DropIndex("dbo.OrderDetails", new[] { "ProductID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Carts", new[] { "CustomerUserId" });
            DropIndex("dbo.CartItems", new[] { "ProductID" });
            DropIndex("dbo.CartItems", new[] { "CartID" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            DropTable("dbo.CustomerUsers");
            DropTable("dbo.Carts");
            DropTable("dbo.CartItems");
            DropTable("dbo.AdminUsers");
        }
    }
}
