namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescriptionProductImageProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Price = c.Single(nullable: false),
                        Weight = c.Single(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        DescriptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Descriptions", t => t.DescriptionId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.DescriptionId);
            
            CreateTable(
                "dbo.Descriptions",
                c => new
                    {
                        DescriptionId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.DescriptionId);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        ProductImageId = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Title = c.String(maxLength: 30),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductImageId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "DescriptionId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "DescriptionId", "dbo.Descriptions");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            AlterColumn("dbo.Categories", "Name", c => c.String());
            DropTable("dbo.ProductImages");
            DropTable("dbo.Descriptions");
            DropTable("dbo.Products");
        }
    }
}
