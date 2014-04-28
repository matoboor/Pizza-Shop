namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        OrderStatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.OrderStatusId);
            
            CreateTable(
                "dbo.OrderItemOptionValues",
                c => new
                    {
                        OrderItemOptionValueId = c.Int(nullable: false, identity: true),
                        OrderItemId = c.Int(nullable: false),
                        ProductOptionId = c.Int(nullable: false),
                        ProductOptionValueId = c.Int(nullable: false),
                        Name = c.String(),
                        Value = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.OrderItemOptionValueId)
                .ForeignKey("dbo.OrderItems", t => t.OrderItemId, cascadeDelete: true)
                .ForeignKey("dbo.ProductOptions", t => t.ProductOptionId, cascadeDelete: true)
                .ForeignKey("dbo.ProductOptionValues", t => t.ProductOptionValueId, cascadeDelete: false)
                .Index(t => t.OrderItemId)
                .Index(t => t.ProductOptionId)
                .Index(t => t.ProductOptionValueId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Name = c.String(),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.OrderItemId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: false)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderStatusId = c.Int(nullable: false),
                        User = c.String(),
                        Name = c.String(),
                        Mail = c.String(),
                        Adress = c.String(),
                        Phone = c.String(),
                        Status = c.String(),
                        Total = c.Double(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.OrderStatus", t => t.OrderStatusId, cascadeDelete: true)
                .Index(t => t.OrderStatusId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "OrderStatusId" });
            DropIndex("dbo.OrderItems", new[] { "ProductId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.OrderItemOptionValues", new[] { "ProductOptionValueId" });
            DropIndex("dbo.OrderItemOptionValues", new[] { "ProductOptionId" });
            DropIndex("dbo.OrderItemOptionValues", new[] { "OrderItemId" });
            DropForeignKey("dbo.Orders", "OrderStatusId", "dbo.OrderStatus");
            DropForeignKey("dbo.OrderItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderItemOptionValues", "ProductOptionValueId", "dbo.ProductOptionValues");
            DropForeignKey("dbo.OrderItemOptionValues", "ProductOptionId", "dbo.ProductOptions");
            DropForeignKey("dbo.OrderItemOptionValues", "OrderItemId", "dbo.OrderItems");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.OrderItemOptionValues");
            DropTable("dbo.OrderStatus");
        }
    }
}
