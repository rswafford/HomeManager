namespace HomeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableOwnerKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "OwnerKey", "dbo.Users");
            DropForeignKey("dbo.Images", "OwnerKey", "dbo.Users");
            DropIndex("dbo.Movies", new[] { "OwnerKey" });
            DropIndex("dbo.Images", new[] { "OwnerKey" });
            AlterColumn("dbo.Movies", "OwnerKey", c => c.Guid());
            AlterColumn("dbo.Images", "OwnerKey", c => c.Guid());
            AddForeignKey("dbo.Movies", "OwnerKey", "dbo.Users", "Key");
            AddForeignKey("dbo.Images", "OwnerKey", "dbo.Users", "Key");
            CreateIndex("dbo.Movies", "OwnerKey");
            CreateIndex("dbo.Images", "OwnerKey");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Images", new[] { "OwnerKey" });
            DropIndex("dbo.Movies", new[] { "OwnerKey" });
            DropForeignKey("dbo.Images", "OwnerKey", "dbo.Users");
            DropForeignKey("dbo.Movies", "OwnerKey", "dbo.Users");
            AlterColumn("dbo.Images", "OwnerKey", c => c.Guid(nullable: false));
            AlterColumn("dbo.Movies", "OwnerKey", c => c.Guid(nullable: false));
            CreateIndex("dbo.Images", "OwnerKey");
            CreateIndex("dbo.Movies", "OwnerKey");
            AddForeignKey("dbo.Images", "OwnerKey", "dbo.Users", "Key", cascadeDelete: true);
            AddForeignKey("dbo.Movies", "OwnerKey", "dbo.Users", "Key", cascadeDelete: true);
        }
    }
}
