using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebStoreCoreApplicatioc.DAL;
using WebStoreBakulin.Services.Data;

namespace WebStoreCoreApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    WebStoreContext context = services.GetRequiredService<WebStoreContext>();
                    DbInitializer.Initialize(context);
                    DbInitializer.InitializeUsers(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "ERROR!!! FOR DB INITIALIZING!!!");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseSerilog((host, log) => log.ReadFrom.Configuration(host.Configuration)
                           .MinimumLevel.Debug()
                           .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                           .Enrich.FromLogContext()
                           .WriteTo.Console(
                                outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}]{SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}")
                           .WriteTo.RollingFile($@".\Log\WebStore[{DateTime.Now:yyyy-MM-ddTHH-mm-ss}].log")
                           .WriteTo.File(new JsonFormatter(",", true), $@".\Log\WebStore[{DateTime.Now:yyyy-MM-ddTHH-mm-ss}].log.json")
                           .WriteTo.Seq("http://localhost:5341/"));
                });
    }
}
