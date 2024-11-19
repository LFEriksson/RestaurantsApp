using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(
    IRestaurantsRepository restaurantsRepository,
    ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant with {RestaurantId}", request.Id);
        var result = await restaurantsRepository.GetByIdAsync(request.Id);
        if (result == null)
        {
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        }

        if (!restaurantAuthorizationService.Authorize(result, ResourceOperation.Delete))
        {
            throw new NotAuthorizedException();
        }

        await restaurantsRepository.DeleteAsync(result);
    }
}
