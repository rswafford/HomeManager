namespace HomeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMediaHash : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "FileHash", c => c.String());
            AddColumn("dbo.Images", "FileHash", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "FileHash");
            DropColumn("dbo.Movies", "FileHash");
        }
    }
}
