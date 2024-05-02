namespace Insurance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrder : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "PaymentStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "PaymentStatus", c => c.String(nullable: false));
        }
    }
}
