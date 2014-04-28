namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductOptions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductOptions",
                c => new
                    {
                        ProductOptionId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        ProductId = c.Int(nullable: false),
                        OptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductOptionId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Options", t => t.OptionId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.OptionId);
            
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        OptionId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OptionId);
            
            CreateTable(
                "dbo.OptionValues",
                c => new
                    {
                        OptionValueId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Image = c.String(),
                        OptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OptionValueId)
                .ForeignKey("dbo.Options", t => t.OptionId, cascadeDelete: true)
                .Index(t => t.OptionId);
            
            CreateTable(
                "dbo.ProductOptionValues",
                c => new
                    {
                        ProductOptionValueId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Default = c.Boolean(nullable: false),
                        ProductOptionId = c.Int(nullable: false),
                        OptionValueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductOptionValueId)
                .ForeignKey("dbo.ProductOptions", t => t.ProductOptionId, cascadeDelete: true)
                .ForeignKey("dbo.OptionValues", t => t.OptionValueId, cascadeDelete: false)
                .Index(t => t.ProductOptionId)
                .Index(t => t.OptionValueId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductOptionValues", new[] { "OptionValueId" });
            DropIndex("dbo.ProductOptionValues", new[] { "ProductOptionId" });
            DropIndex("dbo.OptionValues", new[] { "OptionId" });
            DropIndex("dbo.ProductOptions", new[] { "OptionId" });
            DropIndex("dbo.ProductOptions", new[] { "ProductId" });
            DropForeignKey("dbo.ProductOptionValues", "OptionValueId", "dbo.OptionValues");
            DropForeignKey("dbo.ProductOptionValues", "ProductOptionId", "dbo.ProductOptions");
            DropForeignKey("dbo.OptionValues", "OptionId", "dbo.Options");
            DropForeignKey("dbo.ProductOptions", "OptionId", "dbo.Options");
            DropForeignKey("dbo.ProductOptions", "ProductId", "dbo.Products");
            DropTable("dbo.ProductOptionValues");
            DropTable("dbo.OptionValues");
            DropTable("dbo.Options");
            DropTable("dbo.ProductOptions");
        }
    }
}
