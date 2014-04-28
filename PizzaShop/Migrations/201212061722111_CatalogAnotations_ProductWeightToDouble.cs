namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CatalogAnotations_ProductWeightToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Weight", c => c.Double(nullable: false));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Products", "Weight", c => c.Single(nullable: false));
        }
    }
}
