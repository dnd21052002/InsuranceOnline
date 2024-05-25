namespace Insurance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeStatusClaim : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Claims", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Claims", "Status", c => c.Boolean(nullable: false));
        }
    }
}
