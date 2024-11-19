using API.Middlewares;
using Application.Restaurants.Commands.CreateRestaurantCommand;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;

namespace API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearerAuth",
                        },
                    },
                    Array.Empty<string>()
                },
            });
        });
        builder.Services.AddAuthentication();
        builder.Services.AddIdentityApiEndpoints<AppUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();

        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateRestaurantCommandValidator));
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration)
        );
    }
}
