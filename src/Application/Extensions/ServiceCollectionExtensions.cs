using Application.Dishes.Commands.CreateDish;
using Application.Restaurants.Commands.CreateRestaurantCommand;
using Application.User;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddScoped<IValidator<CreateRestaurantCommand>, CreateRestaurantCommandValidator>();
        services.AddScoped<IValidator<CreateDishCommand>, CreateDishCommandValidator>();
        services.AddScoped<IUserContext, UserContext>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));


    }
}
