using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Net;
using Xunit;

namespace API.Tests.Controllers;

public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock = new();

    public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(service =>
            {
                service.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                service.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantsRepository),
                                            _ => _restaurantsRepositoryMock.Object));
            });
        });
    }

    [Fact]
    public async Task GetAll_ForValidRequest_Return200Ok()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("api/v1/restaurant?pageNumber=1&pageSize=10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAll_ForInvalidRequest_Return400BadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("api/v1/restaurant?pageNumber=1&pageSize=9999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetById_ForValidRequest_Return200Ok()
    {
        // Arrange
        var id = 1;
        var restaurant = new Restaurant { Id = id, Name = "Test Restaurant" };
        _restaurantsRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync(restaurant);

        var client = _factory.CreateClient();
        // Act
        var response = await client.GetAsync($"api/v1/restaurant/{id}");
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetById_ForInvalidRequest_Return404NotFound()
    {
        // Arrange
        var id = 9999;

        _restaurantsRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync((Restaurant?)null);

        var client = _factory.CreateClient();
        // Act
        var response = await client.GetAsync($"api/v1/restaurant/{id}");
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
