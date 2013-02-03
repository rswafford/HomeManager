namespace HomeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAllDbGeneratedKeys : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.FillUps", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.PaymentMethods", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.Repairs", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.RepairShops", "Key", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RepairShops", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Repairs", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.PaymentMethods", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.FillUps", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Cars", "Key", c => c.Guid(nullable: false, identity: true));
        }
    }
}
