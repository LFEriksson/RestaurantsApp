using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    ILogger<CreateDishCommandHandler> logger,
    IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish: {@DishRequest} for restaurant with id: {restaurantID}", request, request.RestaurantId);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant is null)
        {
            logger.LogWarning("Restaurant with id: {restaurantID} not found", request.RestaurantId);
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }
        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
        {
            throw new NotAuthorizedException();
        }

        var mapper = new DishMapper();
        var dish = mapper.CreateDishCommandToDish(request);

        return await dishesRepository.Create(dish);

    }
}

