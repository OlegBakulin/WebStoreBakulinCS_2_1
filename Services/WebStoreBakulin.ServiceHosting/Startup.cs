﻿using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WebStoreBakulin.Interfaces.Services;
using WebStoreBakulin.Services.Data;
using WebStoreBakulin.Services.Products;
using WebStoreCoreApplicatioc.DAL;
using WebStoreCoreApplication.Controllers.Infrastructure.Services;
using WebStoreCoreApplication.Domain.Entities.Identity;
using WebStoreBakulin.Logger;

namespace WebStoreBakulin.ServiceHosting
{
    public sealed record Startup (IConfiguration configuration)
    {    

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<DbInitializer>();
           


            services.AddIdentity<User, Role>(opt => { })
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
            


            services.AddSingleton<IEmployeeService, InMemoryEmployeeServices>();
            services.AddScoped<IProductServices, SqlProductService>();
            services.AddScoped<IOrdersService, SqlOrdersService>();
            services.AddScoped<ICartService, CookieCartService>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { Title = "WebStoreBakulin.ServiceHosting", 
                    Version = "v1" });

                const string webstore_api_xml = "WebStore.ServiceHosting.xml";
                const string webstore_domain_xml = "WebStore.Domain.xml";
                const string debug_path = "bin/Debug/net5.0";

                //c.IncludeXmlComments(webstore_api_xml);

                if (File.Exists(webstore_domain_xml))
                    c.IncludeXmlComments(webstore_domain_xml);
                else if (File.Exists(Path.Combine(debug_path, webstore_domain_xml)))
                    c.IncludeXmlComments(Path.Combine(debug_path, webstore_domain_xml));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbInitializer db, ILoggerFactory log)
        {
            log.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebStoreBakulin.ServiceHosting v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
