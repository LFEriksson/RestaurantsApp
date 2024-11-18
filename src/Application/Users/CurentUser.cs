namespace Application.User;

public record CurentUser(string UserId, string Email, IEnumerable<string> Roles)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
