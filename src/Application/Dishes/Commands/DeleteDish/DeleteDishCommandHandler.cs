using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Dishes.Commands.DeleteDish;

public class DeleteDishCommandHandler(
    IDishesRepository dishesRepository,
    IRestaurantsRepository restaurantsRepository,
    ILogger<DeleteDishCommandHandler> logger,
    IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteDishCommand>
{
    public async Task Handle(DeleteDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting dish with id {DishId} for restaurant with id {RestaurantId}", request.DishId, request.RestaurantId);
        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant == null)
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }

        var dish = restaurant.Dishes.FirstOrDefault(dish => dish!.Id == request.DishId);
        if (dish == null)
        {
            throw new NotFoundException(nameof(Dish), request.DishId.ToString());
        }

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
        {
            throw new NotAuthorizedException();
        }

        await dishesRepository.Delete(dish);
    }
}
