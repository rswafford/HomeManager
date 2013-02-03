namespace HomeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehicleClasses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        VehicleIdentificationNumber = c.String(),
                        LicensePlateNumber = c.String(nullable: false, maxLength: 8),
                        Make = c.String(nullable: false),
                        Model = c.String(nullable: false),
                        Year = c.String(nullable: false, maxLength: 4),
                        PurchaseDate = c.DateTime(nullable: false),
                        PurchasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellDate = c.DateTime(),
                        SellPrice = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Key);
            
            CreateTable(
                "dbo.FillUps",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        CarKey = c.Guid(nullable: false),
                        PaymentMethodKey = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Gallons = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalMileage = c.Int(nullable: false),
                        FullTank = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Cars", t => t.CarKey, cascadeDelete: true)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethodKey, cascadeDelete: true)
                .Index(t => t.CarKey)
                .Index(t => t.PaymentMethodKey);
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
            CreateTable(
                "dbo.Repairs",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        RepairShopKey = c.Guid(nullable: false),
                        EstimateDate = c.DateTime(),
                        RepairStartDate = c.DateTime(),
                        RepairCompleteDate = c.DateTime(),
                        Estimate = c.Decimal(precision: 18, scale: 2),
                        TotalCost = c.Decimal(precision: 18, scale: 2),
                        Description = c.String(),
                        Invoice = c.Binary(),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.RepairShops", t => t.RepairShopKey, cascadeDelete: true)
                .Index(t => t.RepairShopKey);
            
            CreateTable(
                "dbo.RepairShops",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        Name = c.String(),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        WebsiteUrl = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Repairs", new[] { "RepairShopKey" });
            DropIndex("dbo.FillUps", new[] { "PaymentMethodKey" });
            DropIndex("dbo.FillUps", new[] { "CarKey" });
            DropForeignKey("dbo.Repairs", "RepairShopKey", "dbo.RepairShops");
            DropForeignKey("dbo.FillUps", "PaymentMethodKey", "dbo.PaymentMethods");
            DropForeignKey("dbo.FillUps", "CarKey", "dbo.Cars");
            DropTable("dbo.RepairShops");
            DropTable("dbo.Repairs");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.FillUps");
            DropTable("dbo.Cars");
        }
    }
}
