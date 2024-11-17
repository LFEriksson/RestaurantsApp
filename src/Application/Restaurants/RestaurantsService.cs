using Application.Restaurants.Dtos;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Restaurants;

internal class RestaurantsService(
    IRestaurantsRepository restaurantsRepository,
    IValidator<CreateRestaurantDto> validator,
    ILogger<RestaurantsService> logger) : IRestaurantsService
{
    public async Task<RestaurantDto> CreateRestaurant(CreateRestaurantDto createRestaurantDto)
    {

        logger.LogInformation("Creating restaurant");
        var validationResult = await validator.ValidateAsync(createRestaurantDto);
        if (!validationResult.IsValid)
        {
            logger.LogWarning("Create restaurant dto is not valid");
            throw new ValidationException(validationResult.Errors);
        }
        var mapper = new RestaurantMapper();
        var restaurant = mapper.CreateRestaurantDtoToRestaurant(createRestaurantDto);
        return mapper.RestaurantToRestaurantDto(await restaurantsRepository.AddAsync(restaurant));
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await restaurantsRepository.GetAllAsync();
        var mapper = new RestaurantMapper();
        return restaurants.Select(r => mapper.RestaurantToRestaurantDto(r));

    }

    public async Task<RestaurantDto?> GetRestaurantById(int id)
    {
        logger.LogInformation("Getting restaurant by id {Id}", id);
        var restaurant = await restaurantsRepository.GetByIdAsync(id);
        if (restaurant is null)
        {
            logger.LogWarning("Restaurant with id {Id} was not found", id);
            return null;
        }
        return new RestaurantMapper().RestaurantToRestaurantDto(restaurant);
    }
}
