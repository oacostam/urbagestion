using System.Security.Principal;
using Urbagestion.Model.Models;

namespace Urbagestion.Model.Test
{
    public class TestSecurityExtensions
    {
        public static IPrincipal GetGenericPrincipalAdmin()
        {
            return new GenericPrincipal(new GenericIdentity("Test"), new []{Role.AdminRoleName});
        }
    }
}