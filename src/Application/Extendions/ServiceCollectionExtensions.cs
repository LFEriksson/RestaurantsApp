using Application.Restaurants;
using Application.Restaurants.Dtos;
using Application.Restaurants.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Extendions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddScoped<IRestaurantsService, RestaurantsService>();

        services.AddScoped<IValidator<CreateRestaurantDto>, CreateRestaurantDtoValidator>();

        //services.AddValidatorsFromAssemblyContaining(typeof(CreateRestaurantDtoValidator));
        //services.AddValidatorsFromAssembly(applicationAssembly);

    }
}
