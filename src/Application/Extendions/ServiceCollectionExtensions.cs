using Application.Restaurants;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extendions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantsService, RestaurantsService>();
    }
}
