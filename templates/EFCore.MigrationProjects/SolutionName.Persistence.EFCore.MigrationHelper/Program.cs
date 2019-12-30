using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace SolutionName.Persistence.EFCore.MigrationHelper
{
    public class Program
    {
        public static void Main(string[] args){}
    }


    public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddUserSecrets<Program>();
            var config = builder.Build();
            var services = new ServiceCollection();

            var migrationsAssembly = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(
                x => x.UseSqlServer(config.GetConnectionString("ApplicationConnection"),
                b => b.MigrationsAssembly(migrationsAssembly)
               ));

            var _serviceProvider = services.BuildServiceProvider();
            var db = _serviceProvider.GetService<ApplicationDbContext>();
            return db;
        }
    }
    
    
}
