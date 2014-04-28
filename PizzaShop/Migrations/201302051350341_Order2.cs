namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItemOptionValues", "ProductOptionId", "dbo.ProductOptions");
            DropForeignKey("dbo.OrderItemOptionValues", "ProductOptionValueId", "dbo.ProductOptionValues");
            DropForeignKey("dbo.OrderItems", "ProductId", "dbo.Products");
            DropIndex("dbo.OrderItemOptionValues", new[] { "ProductOptionId" });
            DropIndex("dbo.OrderItemOptionValues", new[] { "ProductOptionValueId" });
            DropIndex("dbo.OrderItems", new[] { "ProductId" });
            AddColumn("dbo.OrderItemOptionValues", "ArchivedProductOptionId", c => c.Int(nullable: false));
            AddColumn("dbo.OrderItemOptionValues", "ArchivedProductOptionValueId", c => c.Int(nullable: false));
            AddColumn("dbo.OrderItems", "ArchivedProductId", c => c.Int(nullable: false));
            DropColumn("dbo.OrderItemOptionValues", "ProductOptionId");
            DropColumn("dbo.OrderItemOptionValues", "ProductOptionValueId");
            DropColumn("dbo.OrderItems", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.OrderItemOptionValues", "ProductOptionValueId", c => c.Int(nullable: false));
            AddColumn("dbo.OrderItemOptionValues", "ProductOptionId", c => c.Int(nullable: false));
            DropColumn("dbo.OrderItems", "ArchivedProductId");
            DropColumn("dbo.OrderItemOptionValues", "ArchivedProductOptionValueId");
            DropColumn("dbo.OrderItemOptionValues", "ArchivedProductOptionId");
            CreateIndex("dbo.OrderItems", "ProductId");
            CreateIndex("dbo.OrderItemOptionValues", "ProductOptionValueId");
            CreateIndex("dbo.OrderItemOptionValues", "ProductOptionId");
            AddForeignKey("dbo.OrderItems", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.OrderItemOptionValues", "ProductOptionValueId", "dbo.ProductOptionValues", "ProductOptionValueId", cascadeDelete: true);
            AddForeignKey("dbo.OrderItemOptionValues", "ProductOptionId", "dbo.ProductOptions", "ProductOptionId", cascadeDelete: true);
        }
    }
}
