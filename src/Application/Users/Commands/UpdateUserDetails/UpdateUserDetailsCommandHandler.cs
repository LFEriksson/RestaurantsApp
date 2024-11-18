using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.User.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommandHandler(
    ILogger<UpdateUserDetailsCommandHandler> logger,
    IUserContext userContext,
    IUserStore<AppUser> userStore) : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurentUser();
        logger.LogInformation("Updating user: {Userid}, with {@Request}", user.UserId, request);

        var appUser = await userStore.FindByIdAsync(user.UserId, cancellationToken);

        if (appUser == null)
        {
            throw new NotFoundException(nameof(AppUser), user.UserId);
        }

        appUser.FirstName = request.FirstName;
        appUser.LastName = request.LastName;
        appUser.DateOfBirth = request.DateOfBirth;

        await userStore.UpdateAsync(appUser, cancellationToken);
    }
}
