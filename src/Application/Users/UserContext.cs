using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.User;

public interface IUserContext
{
    CurentUser GetCurrentUser();
}

public class UserContext(IHttpContextAccessor httpContextAccesor) : IUserContext
{
    public CurentUser GetCurrentUser()
    {
        var user = httpContextAccesor?.HttpContext?.User;
        if (user == null)
        {
            throw new InvalidOperationException("User context is not present");
        }

        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            return null!;
        }

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var email = user.FindFirstValue(ClaimTypes.Email)!;
        var roles = user.FindAll(ClaimTypes.Role).Select(x => x.Value);
        var dateOfBirth = DateOnly.Parse(user.FindFirstValue(ClaimTypes.DateOfBirth)!);

        return new CurentUser(userId, email, roles, dateOfBirth);
    }
}
