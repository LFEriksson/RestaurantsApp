using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Domain.Exceptions;

namespace Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommandHandler(
    ILogger<AssignUserRoleCommandHandler> logger,
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager) : IRequestHandler<AssignUserRoleCommand>
{
    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Assigning user role: {@Request}", request);
        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException(nameof(User), request.UserEmail);

        var role = await roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        var result = await userManager.AddToRoleAsync(user, role.Name!);
        if (!result.Succeeded)
        {
            throw new Exception($"Failed to assign role {role.Name} to user {user.Email}");
        }
    }
}
