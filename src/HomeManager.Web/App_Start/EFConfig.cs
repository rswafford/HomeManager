﻿using System.Data.Entity.Migrations;

namespace HomeManager.Web.App_Start
{
    public class EFConfig
    {
        public static void Initialize()
        {
            RunMigrations();
        }

        private static void RunMigrations()
        {
            var efMigrationSettings = new HomeManager.Domain.Migrations.Configuration();
            var efMigrator = new DbMigrator(efMigrationSettings);

            efMigrator.Update();
        }
    }
}