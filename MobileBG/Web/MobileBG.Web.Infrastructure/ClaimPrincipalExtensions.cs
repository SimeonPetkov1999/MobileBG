namespace MobileBG.Web.Infrastructure;

using System.Security.Claims;

public static class ClaimPrincipalExtensions
{
    public static string GetId(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
