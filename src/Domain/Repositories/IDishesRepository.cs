using Domain.Entities;

namespace Domain.Repositories;

public interface IDishesRepository
{
    Task<int> Create(Dish dish);
    Task Delete(Dish dish);
}
