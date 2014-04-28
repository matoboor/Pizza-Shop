namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class routes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RouteStatus",
                c => new
                    {
                        RouteStatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RouteStatusId);
            
            CreateTable(
                "dbo.RoutePoints",
                c => new
                    {
                        RoutePointId = c.Int(nullable: false, identity: true),
                        RouteId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: true),
                        Address = c.String(),
                        Duration = c.Int(nullable: false),
                        Distance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoutePointId)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .Index(t => t.RouteId);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        RouteId = c.Int(nullable: false, identity: true),
                        RouteStatusId = c.Int(nullable: false),
                        Duration = c.Int(nullable: false),
                        Distance = c.Int(nullable: false),
                        Owner = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RouteId)
                .ForeignKey("dbo.RouteStatus", t => t.RouteStatusId, cascadeDelete: true)
                .Index(t => t.RouteStatusId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Routes", new[] { "RouteStatusId" });
            DropIndex("dbo.RoutePoints", new[] { "RouteId" });
            DropForeignKey("dbo.Routes", "RouteStatusId", "dbo.RouteStatus");
            DropForeignKey("dbo.RoutePoints", "RouteId", "dbo.Routes");
            DropTable("dbo.Routes");
            DropTable("dbo.RoutePoints");
            DropTable("dbo.RouteStatus");
        }
    }
}
