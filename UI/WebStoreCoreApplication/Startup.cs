using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using WebStoreBakulin.Logger;

//using WebStoreCoreApplication.Infrastructure.Services;

namespace WebStoreCoreApplication
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
            services.AddScoped<ICartStore, CookiesCartStore>();
            services.AddScoped<ICartService, CartService>();
            
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            log.AddLog4Net();
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
}

