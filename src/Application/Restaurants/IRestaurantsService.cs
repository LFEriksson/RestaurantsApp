﻿using Application.Restaurants.Dtos;

namespace Application.Restaurants;

public interface IRestaurantsService
{
    Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
    Task<RestaurantDto?> GetRestaurantById(int id);
    Task<RestaurantDto> CreateRestaurant(CreateRestaurantDto createRestaurantDto);
}