namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Route_OrdersCosts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "OrdersCosts", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "OrdersCosts");
        }
    }
}
