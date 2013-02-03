namespace HomeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserAutoGenKeys : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.UserInRoles", "Key", c => c.Guid(nullable: false));
            AlterColumn("dbo.Roles", "Key", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Roles", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.UserInRoles", "Key", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Users", "Key", c => c.Guid(nullable: false, identity: true));
        }
    }
}
