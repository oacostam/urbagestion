﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Urbagestion.Model.Interfaces;
using Urbagestion.Model.Models;

namespace Urbagestion.UI.Web.Test.Class
{
    public static class TestHelper
    {
        public static ClaimsPrincipal GetAdminClaimsPrincipal()
        {
            return new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "TestUser"),
                new Claim(ClaimTypes.Role, Role.AdminRoleName)
            }));
        }

        
    }
}