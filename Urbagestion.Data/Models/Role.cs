using Microsoft.AspNetCore.Identity;

namespace Urbagestion.Model.Models
{
    public class Role : IdentityRole<int>
    {
        public Role()
        {

        }
        public Role(string name):base(name)
        {
        }
    }
}