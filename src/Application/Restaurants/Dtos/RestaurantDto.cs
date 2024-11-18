using Application.Dishes.Dtos;
using System.Collections.ObjectModel;

namespace Application.Restaurants.Dtos;

public class RestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }
    public string? StreetAddress { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }

    public IEnumerable<DishDto> Dishes { get; set; } = new Collection<DishDto>();
}
