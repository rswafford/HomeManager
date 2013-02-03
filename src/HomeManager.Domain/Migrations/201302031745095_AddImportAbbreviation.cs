namespace HomeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImportAbbreviation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "ImportedFormatString", c => c.String(maxLength: 30));
            AlterColumn("dbo.MovieFormats", "Abbreviation", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MovieFormats", "Abbreviation", c => c.String(maxLength: 10));
            DropColumn("dbo.Movies", "ImportedFormatString");
        }
    }
}
