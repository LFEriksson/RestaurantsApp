using Application.Restaurants.Dtos;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Common;

namespace Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(
    IRestaurantsRepository restaurantsRepository,
    ILogger<GetAllRestaurantsQueryHandler> logger) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
{
    public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all restaurants");
        var (restaurants, totalItems) = await restaurantsRepository.GetAllMatchingAsync(
            request.SearchPhrase,
            request.PageNumber,
            request.PageSize,
            request.SortBy,
            request.SortDirection);
        var mapper = new RestaurantMapper();
        var restaurantsDtos = restaurants.Select(restaurant => mapper.RestaurantToRestaurantDto(restaurant));

        var pagedResult = new PagedResult<RestaurantDto>(restaurantsDtos, totalItems, request.PageNumber, request.PageSize);
        return pagedResult;
    }
}
