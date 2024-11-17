using Domain.Entities;

namespace Domain.Repositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(int id);
    Task<int> AddAsync(Restaurant restaurant);
    Task DeleteAsync(Restaurant restaurant);
    Task UpdateAsync(Restaurant restaurant);
}
