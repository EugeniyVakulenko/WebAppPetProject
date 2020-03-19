using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Domain.Utils;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var serviceProvider = services.GetRequiredService<IServiceProvider>();
                var configuration = services.GetRequiredService<IConfiguration>();
                Seed.CreateRoles(serviceProvider, configuration).Wait();
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddDebug();
                logging.AddEventSourceLogger();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                 webBuilder.UseStartup<Startup>();
            });
    }
}
