using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmashHub.Core.Services;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SmashHub
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IDbContext>();
                var migrations = await context.Database.GetPendingMigrationsAsync();

                if (migrations.Count() > 0)
                {
                    Console.WriteLine("Starting to migrate database....");
                    try
                    {
                        await context.Database.MigrateAsync();
                        Console.WriteLine("Database is up to date.");
                    }
                    catch (DbException)
                    {
                        Console.WriteLine("Database Migration failed.");
                        throw;
                    }
                }
            }

            _ = host.RunAsync();
            Console.WriteLine("🚀");
            WebHostExtensions.WaitForShutdown(host);
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args);
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                builder = builder.UseUrls("http://0.0.0.0:5000/;https://0.0.0.0:5001");
            }
            return builder.UseStartup<Startup>();
        }
    }
}
