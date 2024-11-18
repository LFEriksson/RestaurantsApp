using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(
    IRestaurantsRepository restaurantsRepository,
    IValidator<UpdateRestaurantCommand> validator,
    ILogger<UpdateRestaurantCommandHandler> logger) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating restaurant with id: {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            logger.LogWarning("Update restaurant data is not valid");
        }
        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
        if (restaurant == null)
        {
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        }

        restaurant.Name = request.Name;
        restaurant.Description = request.Description;
        restaurant.HasDelivery = request.HasDelivery;

        await restaurantsRepository.UpdateAsync(restaurant);
    }
}
