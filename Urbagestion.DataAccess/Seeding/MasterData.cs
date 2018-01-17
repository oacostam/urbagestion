using System.Data;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Urbagestion.Model.Models;

namespace Urbagestion.DataAccess.Seeding
{
    public static class MasterData
    {
        private static readonly string[] RoleNames = {"Admin", "Manager", "Member"};


        public static void CreateDefaultAdmin(this UserManager<User> userManager)
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

        public static void CreateDefaultRoles(this RoleManager<Role> roleManager)
        {
            foreach (var roleName in RoleNames)
            {
                var roleExist = roleManager.RoleExistsAsync(roleName).Result;
                if (roleExist) continue;
                var roleResult = roleManager.CreateAsync(new Role(roleName)).Result;
                if (roleResult.Succeeded) continue;
                var errors = new StringBuilder();
                foreach (var error in roleResult.Errors)
                    errors.Append($"{error.Code}:{error.Description}");
                throw new DataException(errors.ToString());
            }
        }
    }
}