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
    IValidator<CreateDishCommand> validator,
    IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish: {@DishRequest} for restaurant with id: {restaurantID}", request, request.RestaurantId);
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            logger.LogWarning("Validation failed for {@DishRequest}", request);
            throw new ValidationException(validationResult.Errors);
        }

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

