namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Route_OrdersTotal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "OrdersTotal", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "OrdersTotal");
        }
    }
}
