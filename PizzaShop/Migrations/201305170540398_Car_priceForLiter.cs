namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Car_priceForLiter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "PriceForLiter", c => c.Double(nullable: false));
            AddColumn("dbo.Cars", "PriceForLiter", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "PriceForLiter");
            DropColumn("dbo.Routes", "PriceForLiter");
        }
    }
}
