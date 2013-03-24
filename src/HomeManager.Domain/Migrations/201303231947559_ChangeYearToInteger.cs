namespace HomeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeYearToInteger : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Year", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Year", c => c.String(maxLength: 4));
        }
    }
}
