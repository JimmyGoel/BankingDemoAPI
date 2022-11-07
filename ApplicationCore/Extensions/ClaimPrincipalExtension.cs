using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ApplicationCore.Extensions
{
   public static class ClaimPrincipalExtension
    {
        public static string GetuserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
