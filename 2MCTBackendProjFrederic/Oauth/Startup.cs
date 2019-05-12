using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oauth.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oauth.Models;
using API.Repositories;
using API.Data;
using API.Controllers;

namespace Oauth
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            Configuration = configuration;
            _serviceProvider = serviceProvider;
        }
        public IServiceProvider _serviceProvider { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<Person>().AddRoles<IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<IFestivalRepo, FestivalRepo>();
            services.AddScoped<IReservationRepo, ReservationRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<BackendProjContext>();
            services.AddScoped<usersController>();
            services.AddScoped<reservationsController>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            createRole(serviceProvider).Wait();
        }

        private async Task createRole(IServiceProvider serviceProvider)
        {
            var Rolemanager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<Person>>();

            bool x = await Rolemanager.RoleExistsAsync("Admin");

            if (!x)
            {
                string[] roleNames = { "Admin", "Member" };
                IdentityResult Roleresult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = await Rolemanager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        Roleresult = await Rolemanager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                var poweruser = new Person
                {
                    UserName = "Docent@MCT",
                    Email = "Docent@MCT"
                };
                string userPWD = "Docent@1";
                IdentityResult chkUser = await UserManager.CreateAsync(poweruser, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = await UserManager.AddToRoleAsync(poweruser, "Admin");
                }

            }          


        }
    }
}
