﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SolutionName.ConsoleApp
{
    public class ApplicationDbContext : DbContext
    {
    
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            _preSaveChanges();
            var res = base.SaveChanges();
            _postSaveChanges();
            return res;
        }
      
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            _preSaveChanges();
            var res = await base.SaveChangesAsync(cancellationToken);
            _postSaveChanges();
            return res;
        }

        private void _preSaveChanges()
        {
            _addDateTimeStamps();
        }

        private void _postSaveChanges()
        {
           
        }


        private void _addDateTimeStamps()
        {
            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                var now = DateTime.UtcNow;

                if (item.State == EntityState.Added && item.Metadata.FindProperty("CreatedAt") != null)
                    item.Property("CreatedAt").CurrentValue = now;
                else if (item.State == EntityState.Added && item.Metadata.FindProperty("CreatedAtUtc") != null)
                    item.Property("CreatedAtUtc").CurrentValue = now;

                if (item.Metadata.FindProperty("UpdatedAt") != null)
                    item.Property("UpdatedAt").CurrentValue = now;
                else if (item.Metadata.FindProperty("UpdatedAtUtc") != null)
                    item.Property("UpdatedAtUtc").CurrentValue = now;
            }
        }

    }


}


