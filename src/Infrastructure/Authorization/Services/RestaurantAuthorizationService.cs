using Application.User;
using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService(
    ILogger<RestaurantAuthorizationService> logger,
    IUserContext userContext) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurentUser();
        logger.LogInformation("{UserName} [{UserID}] is trying to {ResourceOperation} restaurant {RestaurantName} [{RestaurantID}]", user.Email, user.UserId, resourceOperation, restaurant.Name, restaurant.Id);

        if (resourceOperation == ResourceOperation.Create || resourceOperation == ResourceOperation.Read)
        {
            logger.LogInformation("Create/read operation - successful authorization");
            return true;
        }
        if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Admin user, Delete operation - successful authorization");
            return true;
        }

        if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update) && restaurant.OwnerId == user.UserId)
        {
            logger.LogInformation("Owner user, Delete operation - successful authorization");
            return true;
        }

        return false;
    }
}
