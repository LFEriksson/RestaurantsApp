﻿using MediatR;

namespace Application.Restaurants.Commands.CreateRestaurantCommand;

public class CreateRestaurantCommand : IRequest<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }
    public string? StreetAddress { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
}