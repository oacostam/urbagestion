using System.Security.Principal;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            //Security services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(
                provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

            // Order matters. 
            // Adds config as singleton.
            services.AddSingleton(configuration);
            // DbContext, needed by services.
            services.AddDbContext<UrbagestionDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IUnitOfWork, UrbagestionDbContext>();
            // Setup seccurity.
            services.AddIdentity<User, Role>(a =>
                {
                    a.User.RequireUniqueEmail = true;
                    a.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<UrbagestionDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<IUserClaimsPrincipalFactory<User>, CustomClaimsPrincipalFactory<User>>();
            // Add application services as transients.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IFacilityManagement, FacilityManagement>();
            // Add automapper service.
            services.AddAutoMapper();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
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

            if(env.IsProduction())
                app.UseResponseCompression();

            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });




            // Migrate and seed db
            //using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    if (serviceScope.ServiceProvider.GetService<UrbagestionDbContext>().AllMigrationsApplied()) return;
            //    serviceScope.ServiceProvider.GetService<UrbagestionDbContext>().Database.Migrate();
            //    serviceScope.ServiceProvider.GetService<RoleManager<Role>>().CreateDefaultRoles();
            //    serviceScope.ServiceProvider.GetService<UserManager<User>>().CreateDefaultAdmin();
            //    serviceScope.ServiceProvider.GetService<UrbagestionDbContext>().Seed();
            //}
        }
    }
}