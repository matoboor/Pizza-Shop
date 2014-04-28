namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Route_CarConsumption : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "CarConsumption", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "CarConsumption");
        }
    }
}
