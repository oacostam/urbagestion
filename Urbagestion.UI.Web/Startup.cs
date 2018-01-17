using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Urbagestion.DataAccess;
using Urbagestion.DataAccess.Extensions;
using Urbagestion.DataAccess.Seeding;
using Urbagestion.Model.Bussines.Implementation;
using Urbagestion.Model.Bussines.Interfaces;
using Urbagestion.Model.Interfaces;
using Urbagestion.Model.Models;
using Urbagestion.UI.Web.Security;
using Urbagestion.UI.Web.Services;

namespace Urbagestion.UI.Web
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(configuration);
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, Role>(a => a.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IUserClaimsPrincipalFactory<User>, CustomClaimsPrincipalFactory<User>>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IUnitOfWork, ApplicationDbContext>();
            services.AddTransient<IFacilityManagement, FacilityManagement>();
            // Add automapper service.
            services.AddAutoMapper();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
            // Migrate and seed db
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                if (serviceScope.ServiceProvider.GetService<ApplicationDbContext>().AllMigrationsApplied()) return;
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
                serviceScope.ServiceProvider.GetService<RoleManager<Role>>().CreateDefaultRoles();
                serviceScope.ServiceProvider.GetService<UserManager<User>>().CreateDefaultAdmin();
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Seed();
            }
        }
    }
}