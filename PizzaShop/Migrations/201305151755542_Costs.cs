namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Costs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Costs", c => c.Double(nullable: false));
            AddColumn("dbo.OrderItems", "Costs", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItems", "Costs");
            DropColumn("dbo.Products", "Costs");
        }
    }
}
