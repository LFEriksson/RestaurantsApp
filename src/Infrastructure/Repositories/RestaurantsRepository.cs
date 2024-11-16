using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class RestaurantsRepository(ResaturantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<Restaurant> AddAsync(Restaurant restaurant)
    {
        await dbContext.Restaurants.AddAsync(restaurant);
        await dbContext.SaveChangesAsync();
        return await Task.FromResult(restaurant);
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        return await dbContext.Restaurants.AsNoTracking().ToListAsync();
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        return await dbContext.Restaurants
            .AsNoTracking()
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(r => r.Id == id!);
    }
}
