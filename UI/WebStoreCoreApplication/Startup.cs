using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStoreCoreApplicatioc.DAL;
using WebStoreCoreApplication.Controllers.Infrastructure;
using WebStoreBakulin.Interfaces.Services;
using WebStoreCoreApplication.Controllers.Infrastructure.Services;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreBakulin.Interfaces.TestApi;
using WebStoreBakulin.Clients.Value;
using WebStoreBakulin.Services.Data;
using WebStoreBakulin.Services.Products;
using WebStoreCoreApplication.Domain.Entities.Identity;
using System.Security.Principal;
using WebStoreBakulin.Clients.Identity;
using WebStoreBakulin.Clients.Employees;
using WebStoreBakulin.Clients.Orders;
using WebStoreBakulin.Clients.Products;

//GenericPrincipal
//WindowsPrincipal
//using WebStoreCoreApplication.Infrastructure.Services;

namespace WebStoreCoreApplication

{
    public sealed record Startup(IConfiguration Configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
               .AddIdentity<User, Role>()
               .AddDefaultTokenProviders();

            #region Custom Identity clients stores

            services
               .AddTransient<IUserStore<User>, UsersClient>()
               .AddTransient<IUserRoleStore<User>, UsersClient>()
               .AddTransient<IUserPasswordStore<User>, UsersClient>()
               .AddTransient<IUserEmailStore<User>, UsersClient>()
               .AddTransient<IUserPhoneNumberStore<User>, UsersClient>()
               .AddTransient<IUserTwoFactorStore<User>, UsersClient>()
               .AddTransient<IUserClaimStore<User>, UsersClient>()
               .AddTransient<IUserLoginStore<User>, UsersClient>();

            services
               .AddTransient<IRoleStore<Role>, RolesClient>();

            #endregion

            services.Configure<IdentityOptions>(opt =>
            {
#if DEBUG
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;

#endif
                opt.User.RequireUniqueEmail = false;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "WebStore-GB";
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;
            });

            services
               .AddControllersWithViews()
               .AddRazorRuntimeCompilation();

            services.AddScoped<IEmployeeService, EmployeesClient>();
            services.AddScoped<IProductServices, ProductsClient>();
            services.AddScoped<ICartService, CookieCartService>();
            services.AddScoped<IOrdersService, OrdersClient>();

            services.AddTransient<IValueService, ValueClient>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWelcomePage("/welcome");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Base}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Base}/{action=Index}/{id?}"
                );
            });
        }
    }
}


/*
{
public sealed record Startup (IConfiguration _configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddDbContext<WebStoreContext>(options => options
            .UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

        services.AddTransient<DbInitializer>();
        services.AddMvc(option =>
        {
            option.Filters.Add(typeof(SimpleActionFilter));
        });

        services.AddSingleton<IEmployeeService, InMemoryEmployeeServices>();
        services.AddScoped<IProductServices, SqlProductService>();
        services.AddScoped<IOrdersService, SqlOrdersService>();

        services.AddSingleton<IValueService, ValueClient>();



        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<WebStoreContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options => // необязательно
        {
            // Password settings
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 5;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;


            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            options.Lockout.MaxFailedAccessAttempts = 10;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.RequireUniqueEmail = true;
        });

        services.ConfigureApplicationCookie(options => // необязательно
        {
            // Cookie settings
            options.Cookie.HttpOnly = true;
            //options.Cookie.Expiration = TimeSpan.FromDays(150);
            options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
            options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
            options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
            options.SlidingExpiration = true;
        });
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<ICartService, CookieCartService>();
        //services.AddScoped<UserOrderViewModel>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        var hello = _configuration["CustomHelloWorld"];

        app.Map("/index", CustomIndexHandler);

        UseMiddlewareSample(app);

        app.UseMiddleware<TokenMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Base}/{action=Index}/{id?}");

            //endpoints.MapDefaultControllerRoute(); 
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Base}/{action=Index}/{id?}");

            //endpoints.MapGet("/", async context =>
            //{
            //    await context.Response.WriteAsync(hello);
            //});
        });

        app.Run(async (context) =>
        {
            await context.Response.WriteAsync("Привет из конвейера обработки запроса (метод app.Run())");
        });
    }

    private void UseMiddlewareSample(IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            bool isError = false;
            // ...
            if (isError)
            {
                await context.Response
                    .WriteAsync("Error occured. You're in custom pipeline module...");
            }
            else
            {
                await next.Invoke();
            }

        });
    }

    private void CustomIndexHandler(IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            await context.Response.WriteAsync("Index custom handler...");
        });
    }
}
}*/

