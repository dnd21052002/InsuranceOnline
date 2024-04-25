namespace Insurance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProductCus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ExpireType", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "ExpireTime", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "ExpireTime");
            DropColumn("dbo.Products", "ExpireType");
        }
    }
}
