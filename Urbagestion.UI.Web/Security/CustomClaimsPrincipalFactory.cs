using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Urbagestion.Model.Models;

namespace Urbagestion.UI.Web.Security
{
    public class CustomClaimsPrincipalFactory<TUser> : UserClaimsPrincipalFactory<TUser> where TUser : User
    {
        public CustomClaimsPrincipalFactory(
            UserManager<TUser> userManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(TUser user)
        {
            var id = await base.GenerateClaimsAsync(user);
            if (!string.IsNullOrEmpty(user.Addresss))
            {
                id.AddClaim(new Claim(CustomClaims.Address, user.Addresss));
            }
            return id;
        }
    }
}