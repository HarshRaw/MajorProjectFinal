using MajorProject.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using MajorProject.Data;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace MajorProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MajorProjectDbContext>((options) =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DConnectionString"));
            });

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<MajorProjectDbContext>()
                .AddDefaultTokenProviders();
            //.AddDefaultIdentity<IdentityUser>(options=> options.SignIn.RequireConfirmedAccount=true)
            //.AddEntityFrameworkStores<MajorProjectDbContext>()
            //.AddDefaultTokenProviders();

            // Register the Razor View Engine to provide support for Razor Pages.
            // And Register the Authorization Policy to the Area OR Page pertaining to Razor Pages in the Area(s).
            services
                .AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });

            // Configure the Application Cookie options
            services
                .ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = "/Identity/Account/Login";
                    options.LogoutPath = "/Identity/Account/Logout";
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);      // Default Session Cookie expiry is 20 minutes logout
                    options.SlidingExpiration = true;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.Name = "MajorProjectCookies";
                });


            services.AddMvc();


            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Major Project",
                    Description = "OnMyWayVehicleService - Project"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure
            (IApplicationBuilder app, IWebHostEnvironment env,
            RoleManager<IdentityRole> rm,//Role Manager rm
            UserManager<IdentityUser> um) //UserManager um
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Add the Swagger Middleware
                app.UseSwagger();

                // Add the Swagger Documentation Generation Middleware
                app.UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("/swagger/v1/swagger.json", "My Web API v1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            // Activate the OWIN Middleware to use Authentication and Authorization Services.
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area}/{controller}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });

            MajorProjectDbContextSeed.SeedIdentityRoleAsync(rm).Wait();
            MajorProjectDbContextSeed.SeedIdentityUserAsync(um).Wait();
        }
    }
}
