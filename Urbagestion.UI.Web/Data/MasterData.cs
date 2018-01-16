using System.Data;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Urbagestion.Model.Models;

namespace Urbagestion.UI.Web.Data
{
    public class MasterData
    {
        private static readonly string[] RoleNames = {"Admin", "Manager", "Member"};

        public static void SeedDefaultUsersAndRoles(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            CreateRoles(roleManager);
            CreateAdmin(userManager);
        }

        private static void CreateAdmin(UserManager<User> userManager)
        {
            var user = userManager.FindByNameAsync("AdminUrbagestion").Result;
            if (user == null)
            {
                user = new User {UserName = "Admin", Email = "admin@none.com"};
                userManager.CreateAsync(user, "Admin123-").Wait();
                userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Admin")).Wait();
                userManager.AddClaimAsync(user, new Claim("ManageStore", "Allowed")).Wait();
            }
        }

        private static async Task CreateRoles(RoleManager<Role> roleManager)
        {
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