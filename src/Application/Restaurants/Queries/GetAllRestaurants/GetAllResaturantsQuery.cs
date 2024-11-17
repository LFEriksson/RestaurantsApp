﻿using Application.Restaurants.Dtos;
using MediatR;

namespace Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllResaturantsQuery : IRequest<IEnumerable<RestaurantDto>>
{
}
