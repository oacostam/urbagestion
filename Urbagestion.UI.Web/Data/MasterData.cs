using System;
using System.Data;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Urbagestion.DataAccess;
using Urbagestion.Model.Models;

namespace Urbagestion.UI.Web.Data
{
    public class MasterData
    {
        private static readonly string[] RoleNames = {"Admin", "Manager", "Member"};

        public static async Task InitializeFacilityeDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var db = scopeServiceProvider.GetService<ApplicationDbContext>();
                if (await db.Database.EnsureCreatedAsync())
                {
                    await CreateRoles(scopeServiceProvider);
                    await CreateAdmin(scopeServiceProvider);
                }
            }
        }

        private static async Task CreateAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<User>>();
            var user = await userManager.FindByNameAsync("AdminUrbagestion");
            if (user == null)
            {
                user = new User { UserName = "Admin", Email = "admin@none.com"};
                
                await userManager.CreateAsync(user, "Admin123-");
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Admin"));
                await userManager.AddClaimAsync(user, new Claim("ManageStore", "Allowed"));
            }
        }

        private static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            foreach (var roleName in RoleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (roleExist) continue;
                var roleResult = await roleManager.CreateAsync(new Role(roleName));
                if (roleResult.Succeeded) continue;
                var errors = new StringBuilder();
                foreach (var error in roleResult.Errors) 
                    errors.Append($"{error.Code}:{error.Description}");
                throw new DataException(errors.ToString());
            }
        }
    }
}