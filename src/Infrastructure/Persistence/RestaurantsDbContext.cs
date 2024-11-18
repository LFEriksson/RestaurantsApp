using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class RestaurantsDbContext : IdentityDbContext<AppUser>
{
    public RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : base(options)
    {
    }
    public DbSet<Restaurant> Restaurants { get; set; } = default!;
    public DbSet<Dish> Dishes { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>()
            .OwnsOne(r => r.Address);
    }
}
