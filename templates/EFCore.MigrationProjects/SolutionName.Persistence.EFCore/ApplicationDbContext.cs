using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

//using SolutionName.Domain;

namespace SolutionName.Persistence.EFCore
{
    
    public class ApplicationDbContext : DbContext
    {
       
        //public DbSet<EntityOne> EntityOness { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            _preSaveChanges();
            var res = base.SaveChanges();
            return res;
        }
      
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            _preSaveChanges();
            var res = await base.SaveChangesAsync(cancellationToken);
            return res;
        }

        private void _preSaveChanges()
        {
            _addDateTimeStamps();
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


