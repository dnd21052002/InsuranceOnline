namespace Insurance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProductCus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductCustomers", "ExpireTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductCustomers", "ExpireTime", c => c.DateTime(nullable: false));
        }
    }
}
