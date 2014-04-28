namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cars : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        Consumption = c.Double(nullable: false),
                        KilometresDriven = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.CarId);
            
            AddColumn("dbo.Routes", "Car", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "Car");
            DropTable("dbo.Cars");
        }
    }
}
