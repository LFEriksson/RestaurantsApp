using Application.Restaurants;
using Application.Restaurants.Commands.CreateRestaurantCommand;
using Application.Restaurants.Dtos;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Extendions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddScoped<IValidator<CreateRestaurantCommand>, CreateRestaurantCommandValidator>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
    }
}
