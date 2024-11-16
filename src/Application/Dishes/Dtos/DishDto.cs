using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Dishes.Dtos;

public class DishDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
}
