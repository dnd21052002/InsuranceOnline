namespace Insurance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDateClaim : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Claims", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Claims", "ApprovedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Claims", "ApprovedDate");
            DropColumn("dbo.Claims", "CreatedDate");
        }
    }
}
