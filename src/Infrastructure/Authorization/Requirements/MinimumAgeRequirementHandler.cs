using Application.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Authorization.Requirements;

public class MinimumAgeRequirementHandler(
    ILogger<MinimumAgeRequirementHandler> logger,
    IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var currentUser = userContext.GetCurentUser();

        logger.LogInformation("User: {Email}, date of birth {Dob} - Handling MinimumRequirement", currentUser.Email, currentUser.DateOfBirth);


        if (currentUser.DateOfBirth!.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Now))
        {
            logger.LogInformation("Authorization granted for {Email}", currentUser.Email);
            context.Succeed(requirement);
        }
        else
        {
            logger.LogInformation("Authorization denied for {Email}", currentUser.Email);
        }

        return Task.CompletedTask;
    }
}
