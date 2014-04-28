namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Statuses_Fix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderStatus", "Permanent", c => c.Boolean());
            AddColumn("dbo.RouteStatus", "Permanent", c => c.Boolean());
            AlterColumn("dbo.OrderStatus", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.RouteStatus", "Name", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RouteStatus", "Name", c => c.String());
            AlterColumn("dbo.OrderStatus", "Name", c => c.String(nullable: false, maxLength: 15));
            DropColumn("dbo.RouteStatus", "Permanent");
            DropColumn("dbo.OrderStatus", "Permanent");
        }
    }
}
