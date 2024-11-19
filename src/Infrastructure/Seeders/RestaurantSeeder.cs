using Domain.Constants;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace Infrastructure.Seeders;

internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Restaurants.Any())
            {
                var restaurants = GetNewRestaurants();
                await dbContext.Restaurants.AddRangeAsync(restaurants);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                await dbContext.Roles.AddRangeAsync(roles);
                await dbContext.SaveChangesAsync();
            }
        }

    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        Collection<IdentityRole> roles = [
            new() { Name=UserRoles.Admin, NormalizedName=UserRoles.Admin.ToUpper()},
            new() { Name=UserRoles.Owner, NormalizedName=UserRoles.Owner.ToUpper()},
            new() { Name=UserRoles.User, NormalizedName=UserRoles.User.ToUpper()}
        ];
        return roles;
    }

    private IEnumerable<Restaurant> GetNewRestaurants()
    {
        Collection<Restaurant> restaurants = [
            new()
            {
                Name = "McDonalds",
                Description = "Burgers, fries & more",
                Category = "Fast Food",
                HasDelivery = true,
                ContactEmail = "Contact@McDonalds.com",
                Dishes =
                [
                    new Dish { Name = "Big Mac", Description = "Tasty burger", Price = 5.99m },
                    new Dish { Name = "French Fries", Description = "Salty goodness", Price = 2.99m }
                ],
                Address = new()
                {
                    City = "New York",
                    Streetaddress = "Broadway 5",
                    ZipCode = "10001"
                }
            },
            new()
            {
                Name = "Pizza Hut",
                Description = "Pizza, pasta & more",
                Category = "Fast Food",
                HasDelivery = true,
                ContactEmail = "Contact@PizzaHut.com",
                Dishes =
                [
                    new Dish { Name = "Pepperoni Pizza", Description = "Pizza with pepperoni", Price = 7.99m },
                    new Dish { Name = "Pasta Carbonara", Description = "Pasta with bacon", Price = 6.99m }
                ],
                Address = new()
                {
                    City = "New York",
                    Streetaddress = "Broadway 6",
                    ZipCode = "10001"
                }
            }
        ];
        return restaurants;
    }
}
