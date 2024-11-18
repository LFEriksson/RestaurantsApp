using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.User;

public interface IUserContext
{
    CurentUser GetCurentUser();
}

public class UserContext(IHttpContextAccessor httpContextAccesor) : IUserContext
{
    public CurentUser GetCurentUser()
    {
        var user = httpContextAccesor?.HttpContext.User;
        if (user == null)
        {
            throw new InvalidOperationException("User is not authenticated");
        }

        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            return null!;
        }

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var email = user.FindFirstValue(ClaimTypes.Email)!;
        var roles = user.FindAll(ClaimTypes.Role).Select(x => x.Value);

        return new CurentUser(userId, email, roles);
    }
}
