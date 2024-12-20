﻿using Domain.Entities;
using MediatR;

namespace Application.Dishes.Commands.DeleteDish;

public class DeleteDishCommand(int restaurantId, int dishId) : IRequest
{
    public int RestaurantId { get; set; } = restaurantId;
    public int DishId { get; set; } = dishId;
}
