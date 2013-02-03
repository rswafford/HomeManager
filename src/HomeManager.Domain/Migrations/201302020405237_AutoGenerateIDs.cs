namespace HomeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AutoGenerateIDs : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.UserInRoles", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Roles", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Cars", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Cars", "VehicleIdentificationNumber", c => c.String(maxLength: 50));
            AlterColumn("dbo.FillUps", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.PaymentMethods", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Repairs", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.RepairShops", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Movies", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.MovieFormats", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.MovieFormats", "Abbreviation", c => c.String(maxLength: 10));
            AlterColumn("dbo.MovieInGenres", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.MovieGenres", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Images", "Key", c => c.Guid(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Images", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.MovieGenres", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.MovieInGenres", "Key", c => c.Guid(nullable: false));
            ;
            AlterColumn("dbo.MovieFormats", "Abbreviation", c => c.String());
            AlterColumn("dbo.MovieFormats", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.Movies", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.RepairShops", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.Repairs", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.PaymentMethods", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.FillUps", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.Cars", "VehicleIdentificationNumber", c => c.String());
            AlterColumn("dbo.Cars", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.Roles", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.UserInRoles", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.Users", "Key", c => c.Guid(nullable: false));
        }
    }
}
