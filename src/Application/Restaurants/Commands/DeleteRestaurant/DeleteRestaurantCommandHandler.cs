using Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(
    IRestaurantsRepository restaurantsRepository,
    ILogger<DeleteRestaurantCommandHandler> logger) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deleting restaurant with {request.Id}");
        var result = await restaurantsRepository.GetByIdAsync(request.Id);
        if (result == null)
        {
            logger.LogWarning("Restaurant not found");
            return false;
        }
        await restaurantsRepository.DeleteAsync(result);
        return true;
    }
}
