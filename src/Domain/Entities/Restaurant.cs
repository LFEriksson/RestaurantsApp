using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }

    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }

    [Required]
    public Address? Address { get; set; } = new Address();
    public ICollection<Dish?> Dishes { get; set; } = new Collection<Dish?>();

    public AppUser Owner { get; set; } = default!;
    public string OwnerId { get; set; } = default!;
}
