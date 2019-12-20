using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using SolutionName.Persistence.EFCore;

namespace SolutionName.ConsoleApp
{
    public class Program
    {
        public static IConfigurationRoot _config;
        public static IServiceProvider _serviceProvider;

        public static async Task Main(string[] args)
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();

            services.AddSingleton(_config);

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(_config.GetSection("Logging"));
                builder.AddDebug();
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(
                    _config.GetConnectionString("ApplicationConnection")
            ));

            _serviceProvider = services.BuildServiceProvider();

            await DoWork();

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }


        public static async Task DoWork()
        {
            var db = _serviceProvider.GetService<ApplicationDbContext>();
            //var orders = await db.Orders.FirstOrDefaultAsync();
        }
    }
}
