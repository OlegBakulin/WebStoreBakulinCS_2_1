using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(host => host
                   .UseStartup<Startup>()
                //.UseUrls("http://localhost:5000")
                //.UseUrls("http://localhost:5001")
                //.UseHttpSys(opt => opt.MaxAccepts = 5)
                //.UseKestrel(opt => opt.ListenAnyIP(5001))
                );
    }
}

/*
public static void Main(string[] args)
{
    CreateHostBuilder(args).Build().Run();
    /*
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
            webBuilder.UseStartup<Startup>();
        });
}
}
*/
