using Application.Restaurants.Dtos;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(
    IRestaurantsRepository repository,
    ILogger<GetRestaurantByIdQueryHandler> logger) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting restaurant by id: {RestaurantId}", request.Id);
        var restaurant = await repository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        var mapper = new RestaurantMapper();
        return mapper.RestaurantToRestaurantDto(restaurant);
    }
}
