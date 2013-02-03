namespace HomeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableFormatKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "FormatKey", "dbo.MovieFormats");
            DropIndex("dbo.Movies", new[] { "FormatKey" });
            AlterColumn("dbo.Movies", "FormatKey", c => c.Guid());
            AddForeignKey("dbo.Movies", "FormatKey", "dbo.MovieFormats", "Key");
            CreateIndex("dbo.Movies", "FormatKey");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Movies", new[] { "FormatKey" });
            DropForeignKey("dbo.Movies", "FormatKey", "dbo.MovieFormats");
            AlterColumn("dbo.Movies", "FormatKey", c => c.Guid(nullable: false));
            CreateIndex("dbo.Movies", "FormatKey");
            AddForeignKey("dbo.Movies", "FormatKey", "dbo.MovieFormats", "Key", cascadeDelete: true);
        }
    }
}
