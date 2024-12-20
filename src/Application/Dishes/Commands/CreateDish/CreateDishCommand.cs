﻿using MediatR;
using System.Text.Json.Serialization;

namespace Application.Dishes.Commands.CreateDish;

public class CreateDishCommand : IRequest<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    [JsonIgnore]
    public int RestaurantId { get; set; }
}
