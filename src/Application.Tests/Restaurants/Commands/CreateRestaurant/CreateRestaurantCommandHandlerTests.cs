using Application.Restaurants.Commands.CreateRestaurantCommand;
using Application.User;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Application.Tests.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandlerTests
{
    [Fact]
    public async Task Handle_ForValidCommand_ReturnCreatedRestaurantId()
    {
        // Arrange
        var mockRestaurantRepository = new Mock<IRestaurantsRepository>();
        var mockLogger = new Mock<ILogger<CreateRestaurantCommandHandler>>();
        var mockUserContext = new Mock<IUserContext>();

        var command = new CreateRestaurantCommand
        {
            Name = "Test Restaurant",
            Description = "Test Description",
            Category = "Test Category",
            HasDelivery = true,
            ContactEmail = "test@example.com",
            ContactNumber = "123456789",
            StreetAddress = "123 Test St",
            City = "Test City",
            ZipCode = "12345"
        };

        var currentUser = new CurentUser("user-123", "user@example.com", new List<string>(), null);
        mockUserContext.Setup(uc => uc.GetCurrentUser()).Returns(currentUser);

        var createdRestaurant = new Restaurant
        {
            Id = 1,
            Name = command.Name,
            Description = command.Description,
            Category = command.Category,
            HasDelivery = command.HasDelivery,
            ContactEmail = command.ContactEmail,
            ContactNumber = command.ContactNumber,
            Address = new Address
            {
                Streetaddress = command.StreetAddress,
                City = command.City,
                ZipCode = command.ZipCode
            },
            OwnerId = currentUser.UserId
        };

        mockRestaurantRepository.Setup(repo => repo.AddAsync(It.IsAny<Restaurant>())).ReturnsAsync(createdRestaurant.Id);

        var handler = new CreateRestaurantCommandHandler(mockRestaurantRepository.Object, mockLogger.Object, mockUserContext.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(createdRestaurant.Id);
        mockRestaurantRepository.Verify(repo => repo.AddAsync(It.IsAny<Restaurant>()), Times.Once);
    }
}
