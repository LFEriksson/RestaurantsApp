using Application.Restaurants.Dtos;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllResaturantsQueryHandler(
    IRestaurantsRepository restaurantsRepository,
    ILogger<GetAllResaturantsQueryHandler> logger) : IRequestHandler<GetAllResaturantsQuery, IEnumerable<RestaurantDto>>
{
    public async Task<IEnumerable<RestaurantDto>> Handle(GetAllResaturantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await restaurantsRepository.GetAllAsync();
        var mapper = new RestaurantMapper();
        return restaurants.Select(restaurant => mapper.RestaurantToRestaurantDto(restaurant));
    }
}
