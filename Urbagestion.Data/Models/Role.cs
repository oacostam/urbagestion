using Microsoft.AspNetCore.Identity;

namespace Urbagestion.Model.Models
{
    public class Role : IdentityRole<int>
    {
        public const string AdminRoleName = "Admin";

        public const string UserRoleName = "User";
        public Role()
        {

        }
        public Role(string name):base(name)
        {
        }
    }
}