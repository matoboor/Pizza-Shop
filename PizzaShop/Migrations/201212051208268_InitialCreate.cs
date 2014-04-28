namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryImageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.CategoryImages", t => t.CategoryImageId, cascadeDelete: true)
                .Index(t => t.CategoryImageId);
            
            CreateTable(
                "dbo.CategoryImages",
                c => new
                    {
                        CategoryImageId = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.CategoryImageId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Categories", new[] { "CategoryImageId" });
            DropForeignKey("dbo.Categories", "CategoryImageId", "dbo.CategoryImages");
            DropTable("dbo.CategoryImages");
            DropTable("dbo.Categories");
        }
    }
}
