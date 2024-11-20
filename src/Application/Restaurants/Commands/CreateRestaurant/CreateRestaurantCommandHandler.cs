using Application.User;
using Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Restaurants.Commands.CreateRestaurantCommand;

internal class CreateRestaurantCommandHandler(
    IRestaurantsRepository restaurantsRepository,
    ILogger<CreateRestaurantCommandHandler> logger,
    IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurentUser();

        logger.LogInformation("{UserName} [{UserID}] is creating restaurant {@restaurant}", currentUser.Email, currentUser.UserId, request);
        var mapper = new RestaurantMapper();
        var restaurant = mapper.CreateRestaurantCommandToRestaurant(request);
        restaurant.OwnerId = currentUser.UserId;
        return await restaurantsRepository.AddAsync(restaurant);
    }

}
