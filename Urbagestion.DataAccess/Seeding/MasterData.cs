using System.Data;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Urbagestion.Model.Models;

namespace Urbagestion.DataAccess.Seeding
{
    public static class MasterData
    {
        public const string RoleAdmin = "Admin";
        public const string RoleManager = "Manager";
        public const string RoleUser = "User";

        private static readonly string[] RoleNames = {RoleAdmin, RoleManager, RoleUser};


        public static void CreateDefaultAdmin(this UserManager<User> userManager)
        {
            var user = userManager.FindByNameAsync("AdminUrbagestion").Result;
            if (user == null)
            {
                user = new User {UserName = "admin@none.com", Email = "admin@none.com", Name = "Admin", IsActive = true};
                userManager.CreateAsync(user, "Admin123").Wait();
                userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Admin")).Wait();
                userManager.AddToRoleAsync(user, RoleAdmin).Wait();
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