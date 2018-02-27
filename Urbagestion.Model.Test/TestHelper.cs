using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Urbagestion.Model.Interfaces;
using Urbagestion.Model.Models;

namespace Urbagestion.Model.Test
{
    public class TestHelper
    {
        public static IPrincipal GetGenericPrincipalAdmin()
        {
            return new GenericPrincipal(new GenericIdentity("Test"), new []{Role.AdminRoleName});
        }

        public static T AddMock<T>(T f, IList<T> fakeRepo) where T : IHasIdentity
        {
            var last = fakeRepo.Any() ? fakeRepo.Max(m => m.Id) : 0;
            f.Id = ++last;
            fakeRepo.Add(f);
            return f;
        }
    }
}