using System.Collections.ObjectModel;

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

    public required Adress Address { get; set; } = new Adress();
    public ICollection<Dish?> Dishes { get; set; } = new Collection<Dish?>();
}
