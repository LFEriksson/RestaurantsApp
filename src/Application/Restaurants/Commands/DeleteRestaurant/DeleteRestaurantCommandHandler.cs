using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(
    IRestaurantsRepository restaurantsRepository,
    ILogger<DeleteRestaurantCommandHandler> logger) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant with {RestaurantId}", request.Id);
        var result = await restaurantsRepository.GetByIdAsync(request.Id);
        if (result == null)
        {
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        }
        await restaurantsRepository.DeleteAsync(result);
    }
}
