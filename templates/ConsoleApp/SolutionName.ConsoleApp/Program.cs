using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using MediatR;

namespace SolutionName.ConsoleApp
{
    public class Program : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private static IConfigurationRoot _config;
        private static IServiceProvider _serviceProvider;
        private static ILogger<Program> _log;

        public static async Task Main(string[] args)
        {
            _init();
           
            _log.LogDebug("Doing Work");
         
            //using (var scope = _serviceProvider.CreateScope())
            //{
            //    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            //    await db.SaveChangesAsync();
            //}


            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
        private static void _init()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();

            services.AddSingleton(_config);

            services.AddLogging(builder => {
                builder.AddConfiguration(_config.GetSection("Logging"));
                //builder.AddConsole();
                builder.AddDebug();
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(
                    _config.GetConnectionString("ApplicationConnection")
            ));

            services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);

            _serviceProvider = services.BuildServiceProvider();
            _log = _serviceProvider.GetService<ILogger<Program>>();

          
        }

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            _init();
            return _serviceProvider.GetService<ApplicationDbContext>();
        }



    }
}
