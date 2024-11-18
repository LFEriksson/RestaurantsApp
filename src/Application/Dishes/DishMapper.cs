using Application.Dishes.Commands.CreateDish;
using Application.Dishes.Dtos;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Dishes;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class DishMapper
{
    public partial Dish CreateDishCommandToDish(CreateDishCommand commandEntity);
    public partial DishDto DishToDishDto(Dish entity);
}
