namespace HomeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixKeyNames : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UserMovies", name: "UserKey", newName: "OwnerKey");
            RenameColumn(table: "dbo.UserTvEpisodes", name: "UserKey", newName: "OwnerKey");
            RenameColumn(table: "dbo.UserImages", name: "UserKey", newName: "OwnerKey");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.UserImages", name: "OwnerKey", newName: "UserKey");
            RenameColumn(table: "dbo.UserTvEpisodes", name: "OwnerKey", newName: "UserKey");
            RenameColumn(table: "dbo.UserMovies", name: "OwnerKey", newName: "UserKey");
        }
    }
}
