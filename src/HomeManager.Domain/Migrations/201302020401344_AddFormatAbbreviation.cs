namespace HomeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFormatAbbreviation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovieFormats", "Abbreviation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovieFormats", "Abbreviation");
        }
    }
}
