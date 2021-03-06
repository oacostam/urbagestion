﻿using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Urbagestion.DataAccess.Extensions
{
    public static class DbContextExtension
    {
        public static bool AllMigrationsApplied(this UrbagestionDbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }


        public static void Seed(this UrbagestionDbContext context)
        {
            // Insert master data
        }
    }
}