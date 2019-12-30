﻿using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using MediatR;

namespace SolutionName.ConsoleApp
{
    public class Program
    {
        private static IConfigurationRoot _config;
        private static IServiceProvider _serviceProvider;
        private static ILogger<Program> _logger;

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

            services.AddLogging(builder => {
                builder.AddConfiguration(_config.GetSection("Logging"));
                builder.AddDebug();
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(
                    _config.GetConnectionString("ApplicationConnection")
            ));

            services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);

            _serviceProvider = services.BuildServiceProvider();
            _logger = _serviceProvider.GetService<ILogger<Program>>();

            await DoWork();

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }


        public static Task DoWork()
        {
            _logger.LogInformation("Doing Work");
            return Task.CompletedTask;
        }

    }
}