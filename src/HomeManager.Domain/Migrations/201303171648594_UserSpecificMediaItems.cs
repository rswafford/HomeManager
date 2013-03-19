namespace HomeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserSpecificMediaItems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "OwnerKey", "dbo.Users");
            DropForeignKey("dbo.Images", "OwnerKey", "dbo.Users");
            DropIndex("dbo.Movies", new[] { "OwnerKey" });
            DropIndex("dbo.Images", new[] { "OwnerKey" });
            CreateTable(
                "dbo.UserMovies",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        UserKey = c.Guid(nullable: false),
                        MovieKey = c.Guid(nullable: false),
                        Filename = c.String(nullable: false, maxLength: 300),
                        FullPath = c.String(nullable: false),
                        ImportedFormatString = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Movies", t => t.MovieKey, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserKey, cascadeDelete: true)
                .Index(t => t.MovieKey)
                .Index(t => t.UserKey);
            
            CreateTable(
                "dbo.TvShows",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        ShowName = c.String(),
                        OriginalAirDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Key);
            
            CreateTable(
                "dbo.TvEpisodes",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        TvShowKey = c.Guid(),
                        Season = c.Int(),
                        Episode = c.Int(),
                        OriginalAirDate = c.DateTime(),
                        FormatKey = c.Guid(),
                        Title = c.String(maxLength: 300),
                        FileHash = c.String(),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.MovieFormats", t => t.FormatKey)
                .ForeignKey("dbo.TvShows", t => t.TvShowKey)
                .Index(t => t.FormatKey)
                .Index(t => t.TvShowKey);
            
            CreateTable(
                "dbo.UserTvEpisodes",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        UserKey = c.Guid(nullable: false),
                        TvEpisodeKey = c.Guid(nullable: false),
                        Filename = c.String(nullable: false, maxLength: 300),
                        FullPath = c.String(nullable: false),
                        ImportedFormatString = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.TvEpisodes", t => t.TvEpisodeKey, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserKey, cascadeDelete: true)
                .Index(t => t.TvEpisodeKey)
                .Index(t => t.UserKey);
            
            CreateTable(
                "dbo.UserImages",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        UserKey = c.Guid(nullable: false),
                        ImageKey = c.Guid(nullable: false),
                        Filename = c.String(nullable: false, maxLength: 300),
                        FullPath = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Images", t => t.ImageKey, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserKey, cascadeDelete: true)
                .Index(t => t.ImageKey)
                .Index(t => t.UserKey);
            
            DropColumn("dbo.Movies", "ImportedFormatString");
            DropColumn("dbo.Movies", "OwnerKey");
            DropColumn("dbo.Movies", "Filename");
            DropColumn("dbo.Movies", "FullPath");
            DropColumn("dbo.Images", "OwnerKey");
            DropColumn("dbo.Images", "Filename");
            DropColumn("dbo.Images", "FullPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "FullPath", c => c.String(nullable: false));
            AddColumn("dbo.Images", "Filename", c => c.String(nullable: false, maxLength: 300));
            AddColumn("dbo.Images", "OwnerKey", c => c.Guid());
            AddColumn("dbo.Movies", "FullPath", c => c.String(nullable: false));
            AddColumn("dbo.Movies", "Filename", c => c.String(nullable: false, maxLength: 300));
            AddColumn("dbo.Movies", "OwnerKey", c => c.Guid());
            AddColumn("dbo.Movies", "ImportedFormatString", c => c.String(maxLength: 30));
            DropIndex("dbo.UserImages", new[] { "UserKey" });
            DropIndex("dbo.UserImages", new[] { "ImageKey" });
            DropIndex("dbo.UserTvEpisodes", new[] { "UserKey" });
            DropIndex("dbo.UserTvEpisodes", new[] { "TvEpisodeKey" });
            DropIndex("dbo.TvEpisodes", new[] { "TvShowKey" });
            DropIndex("dbo.TvEpisodes", new[] { "FormatKey" });
            DropIndex("dbo.UserMovies", new[] { "UserKey" });
            DropIndex("dbo.UserMovies", new[] { "MovieKey" });
            DropForeignKey("dbo.UserImages", "UserKey", "dbo.Users");
            DropForeignKey("dbo.UserImages", "ImageKey", "dbo.Images");
            DropForeignKey("dbo.UserTvEpisodes", "UserKey", "dbo.Users");
            DropForeignKey("dbo.UserTvEpisodes", "TvEpisodeKey", "dbo.TvEpisodes");
            DropForeignKey("dbo.TvEpisodes", "TvShowKey", "dbo.TvShows");
            DropForeignKey("dbo.TvEpisodes", "FormatKey", "dbo.MovieFormats");
            DropForeignKey("dbo.UserMovies", "UserKey", "dbo.Users");
            DropForeignKey("dbo.UserMovies", "MovieKey", "dbo.Movies");
            DropTable("dbo.UserImages");
            DropTable("dbo.UserTvEpisodes");
            DropTable("dbo.TvEpisodes");
            DropTable("dbo.TvShows");
            DropTable("dbo.UserMovies");
            CreateIndex("dbo.Images", "OwnerKey");
            CreateIndex("dbo.Movies", "OwnerKey");
            AddForeignKey("dbo.Images", "OwnerKey", "dbo.Users", "Key");
            AddForeignKey("dbo.Movies", "OwnerKey", "dbo.Users", "Key");
        }
    }
}
