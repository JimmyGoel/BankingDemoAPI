using System.Security.Claims;

namespace ApplicationCore.Extensions
{
    public static class ClaimPrincipalExtension
    {
        public static string GetuserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }
        public static int GetuserId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
