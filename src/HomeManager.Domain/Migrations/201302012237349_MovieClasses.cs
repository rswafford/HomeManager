namespace HomeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieClasses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        ImdbId = c.String(maxLength: 10),
                        MovieDbId = c.String(maxLength: 10),
                        Year = c.String(maxLength: 4),
                        ReleaseDate = c.DateTime(nullable: false),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Outline = c.String(),
                        Plot = c.String(),
                        FormatKey = c.Guid(nullable: false),
                        OwnerKey = c.Guid(nullable: false),
                        Filename = c.String(nullable: false, maxLength: 300),
                        FullPath = c.String(nullable: false),
                        Title = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.MovieFormats", t => t.FormatKey, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.OwnerKey, cascadeDelete: true)
                .Index(t => t.FormatKey)
                .Index(t => t.OwnerKey);
            
            CreateTable(
                "dbo.MovieFormats",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        Description = c.String(),
                        PhysicalMedia = c.Boolean(nullable: false),
                        HighDefinition = c.Boolean(nullable: false),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
            CreateTable(
                "dbo.MovieInGenres",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        MovieKey = c.Guid(nullable: false),
                        GenreKey = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Movies", t => t.MovieKey, cascadeDelete: true)
                .ForeignKey("dbo.MovieGenres", t => t.GenreKey, cascadeDelete: true)
                .Index(t => t.MovieKey)
                .Index(t => t.GenreKey);
            
            CreateTable(
                "dbo.MovieGenres",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        Height = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        Description = c.String(),
                        DateTaken = c.DateTime(nullable: false),
                        OwnerKey = c.Guid(nullable: false),
                        Filename = c.String(nullable: false, maxLength: 300),
                        FullPath = c.String(nullable: false),
                        Title = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Users", t => t.OwnerKey, cascadeDelete: true)
                .Index(t => t.OwnerKey);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Images", new[] { "OwnerKey" });
            DropIndex("dbo.MovieInGenres", new[] { "GenreKey" });
            DropIndex("dbo.MovieInGenres", new[] { "MovieKey" });
            DropIndex("dbo.Movies", new[] { "OwnerKey" });
            DropIndex("dbo.Movies", new[] { "FormatKey" });
            DropForeignKey("dbo.Images", "OwnerKey", "dbo.Users");
            DropForeignKey("dbo.MovieInGenres", "GenreKey", "dbo.MovieGenres");
            DropForeignKey("dbo.MovieInGenres", "MovieKey", "dbo.Movies");
            DropForeignKey("dbo.Movies", "OwnerKey", "dbo.Users");
            DropForeignKey("dbo.Movies", "FormatKey", "dbo.MovieFormats");
            DropTable("dbo.Images");
            DropTable("dbo.MovieGenres");
            DropTable("dbo.MovieInGenres");
            DropTable("dbo.MovieFormats");
            DropTable("dbo.Movies");
        }
    }
}
