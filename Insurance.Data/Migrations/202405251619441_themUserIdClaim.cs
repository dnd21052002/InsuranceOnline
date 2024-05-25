namespace Insurance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themUserIdClaim : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Claims", "UserId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Claims", "UserId");
        }
    }
}
