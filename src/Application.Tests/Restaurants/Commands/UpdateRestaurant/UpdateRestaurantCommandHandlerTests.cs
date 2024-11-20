using Xunit;
using Domain.Repositories;
using Moq;
using Microsoft.Extensions.Logging;
using Domain.Interfaces;
using Domain.Entities;
using Domain.Constants;
using Domain.Exceptions;
using Application.Restaurants.Commands.UpdateRestaurant;

namespace Application.Tests.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandlerTests
{
    private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock;
    private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
    private readonly Mock<IRestaurantAuthorizationService> _restaurantAuthorizationServiceMock;
    private readonly UpdateRestaurantCommandHandler _handler;

    public UpdateRestaurantCommandHandlerTests()
    {
        _restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
        _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        _restaurantAuthorizationServiceMock = new Mock<IRestaurantAuthorizationService>();
        _handler = new UpdateRestaurantCommandHandler(
            _restaurantsRepositoryMock.Object,
            _loggerMock.Object,
            _restaurantAuthorizationServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldUpdateRestaurant()
    {
        // Arrange
        var command = new UpdateRestaurantCommand
        {
            Id = 1,
            Name = "Updated Name",
            Description = "Updated Description",
            HasDelivery = true
        };

        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "Original Name",
            Description = "Original Description",
            HasDelivery = false
        };

        _restaurantsRepositoryMock.Setup(repo => repo.GetByIdAsync(command.Id))
            .ReturnsAsync(restaurant);
        _restaurantAuthorizationServiceMock.Setup(auth => auth.Authorize(restaurant, ResourceOperation.Update))
            .Returns(true);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _restaurantsRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Restaurant>(r =>
            r.Id == command.Id &&
            r.Name == command.Name &&
            r.Description == command.Description &&
            r.HasDelivery == command.HasDelivery)), Times.Once);
    }

    [Fact]
    public async Task Handle_RestaurantNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new UpdateRestaurantCommand { Id = 1 };

        _restaurantsRepositoryMock.Setup(repo => repo.GetByIdAsync(command.Id))
            .ReturnsAsync((Restaurant)null!);

        // Act & Assert
        await Xunit.Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_NotAuthorized_ShouldThrowNotAuthorizedException()
    {
        // Arrange
        var command = new UpdateRestaurantCommand { Id = 1 };

        var restaurant = new Restaurant { Id = 1 };

        _restaurantsRepositoryMock.Setup(repo => repo.GetByIdAsync(command.Id))
            .ReturnsAsync(restaurant);
        _restaurantAuthorizationServiceMock.Setup(auth => auth.Authorize(restaurant, ResourceOperation.Update))
            .Returns(false);

        // Act & Assert
        await Xunit.Assert.ThrowsAsync<NotAuthorizedException>(() => _handler.Handle(command, CancellationToken.None));
    }
}