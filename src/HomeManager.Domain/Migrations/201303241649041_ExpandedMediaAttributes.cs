namespace HomeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpandedMediaAttributes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Tagline", c => c.String());
            AddColumn("dbo.Movies", "Runtime", c => c.Int());
            AddColumn("dbo.Movies", "Top250", c => c.Int());
            AddColumn("dbo.TvShows", "ImdbId", c => c.String(maxLength: 10));
            AddColumn("dbo.TvShows", "TvDbId", c => c.String(maxLength: 10));
            AddColumn("dbo.TvShows", "Zap2ItId", c => c.String(maxLength: 20));
            AddColumn("dbo.TvShows", "Network", c => c.String());
            AddColumn("dbo.TvShows", "Overview", c => c.String());
            AddColumn("dbo.TvShows", "Runtime", c => c.Int());
            AddColumn("dbo.TvEpisodes", "EpisodeName", c => c.String());
            AddColumn("dbo.TvEpisodes", "Overview", c => c.String());
            AddColumn("dbo.TvEpisodes", "ImdbId", c => c.String(maxLength: 10));
            AddColumn("dbo.TvEpisodes", "TvDbId", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TvEpisodes", "TvDbId");
            DropColumn("dbo.TvEpisodes", "ImdbId");
            DropColumn("dbo.TvEpisodes", "Overview");
            DropColumn("dbo.TvEpisodes", "EpisodeName");
            DropColumn("dbo.TvShows", "Runtime");
            DropColumn("dbo.TvShows", "Overview");
            DropColumn("dbo.TvShows", "Network");
            DropColumn("dbo.TvShows", "Zap2ItId");
            DropColumn("dbo.TvShows", "TvDbId");
            DropColumn("dbo.TvShows", "ImdbId");
            DropColumn("dbo.Movies", "Top250");
            DropColumn("dbo.Movies", "Runtime");
            DropColumn("dbo.Movies", "Tagline");
        }
    }
}
