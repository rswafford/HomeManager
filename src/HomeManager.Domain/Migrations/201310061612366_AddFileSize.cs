namespace HomeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileSize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserMovies", "FileSize", c => c.Long(nullable: false));
            AddColumn("dbo.UserTvEpisodes", "FileSize", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTvEpisodes", "FileSize");
            DropColumn("dbo.UserMovies", "FileSize");
        }
    }
}
