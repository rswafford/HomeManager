namespace HomeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDBGeneratedKeysForMedia : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.MovieFormats", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.MovieInGenres", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.MovieGenres", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.Images", "Key", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Images", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.MovieGenres", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.MovieInGenres", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.MovieFormats", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Movies", "Key", c => c.Guid(nullable: false, identity: true));
        }
    }
}
