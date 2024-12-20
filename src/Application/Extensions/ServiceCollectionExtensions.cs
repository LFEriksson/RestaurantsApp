﻿using Application.Dishes.Commands.CreateDish;
using Application.Restaurants.Commands.CreateRestaurantCommand;
using Application.User;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddHttpContextAccessor();

        services.AddFluentValidationAutoValidation();

        services.AddScoped<IUserContext, UserContext>();

    }
}
