using HomeManager.Domain.Entities;

namespace HomeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<HomeManager.Domain.Entities.Core.EntitiesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HomeManager.Domain.Entities.Core.EntitiesContext context)
        {
            context.Roles.AddOrUpdate(role => role.Name,
                new Role { Key = Guid.NewGuid(), Name = "Admin"},
                new Role { Key = Guid.NewGuid(), Name = "Adult" },
                new Role { Key = Guid.NewGuid(), Name = "Child" });
        }
    }
}
