namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order_with_anotations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "Wish", c => c.String());
            AddColumn("dbo.Orders", "FirstName", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.Orders", "LastName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Orders", "Mail", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Adress", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Phone", c => c.String(nullable: false, maxLength: 15));
            DropColumn("dbo.Orders", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Name", c => c.String());
            AlterColumn("dbo.Orders", "Phone", c => c.String());
            AlterColumn("dbo.Orders", "Adress", c => c.String());
            AlterColumn("dbo.Orders", "Mail", c => c.String());
            DropColumn("dbo.Orders", "LastName");
            DropColumn("dbo.Orders", "FirstName");
            DropColumn("dbo.OrderItems", "Wish");
        }
    }
}
