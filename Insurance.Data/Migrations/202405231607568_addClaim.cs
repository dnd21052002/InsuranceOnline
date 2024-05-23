namespace Insurance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addClaim : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClaimDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        FilePath = c.String(),
                        ClaimId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Claims", t => t.ClaimId, cascadeDelete: true)
                .Index(t => t.ClaimId);
            
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        CitizenId = c.String(),
                        InsuranceId = c.Int(nullable: false),
                        InsuranceName = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClaimDocuments", "ClaimId", "dbo.Claims");
            DropIndex("dbo.ClaimDocuments", new[] { "ClaimId" });
            DropTable("dbo.Claims");
            DropTable("dbo.ClaimDocuments");
        }
    }
}
