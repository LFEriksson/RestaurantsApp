using System.ComponentModel.DataAnnotations;

namespace Application.Restaurants.Dtos;

public class CreateRestaurantDto
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = default!;
    [Required]
    [StringLength(500, MinimumLength = 1)]
    public string Description { get; set; } = default!;
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }

    [EmailAddress]
    [StringLength(100, MinimumLength = 1)]
    public string? ContactEmail { get; set; }
    [Phone]
    [StringLength(20, MinimumLength = 1)]
    public string? ContactNumber { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string? StreetAdress { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string? City { get; set; }
    [Required]
    [StringLength(18, MinimumLength = 1)]
    public string? ZipCode { get; set; }
}
