namespace Insurance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeProductCate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductCategories", "Alias", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductCategories", "Alias", c => c.String(nullable: false, maxLength: 256));
        }
    }
}
