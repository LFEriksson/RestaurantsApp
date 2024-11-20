using Xunit;
using FluentValidation.TestHelper;
using Application.Restaurants.Commands.UpdateRestaurant;

namespace Application.Tests.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantValidatorTests
{
    private readonly UpdateRestaurantValidator _validator;

    public UpdateRestaurantValidatorTests()
    {
        _validator = new UpdateRestaurantValidator();
    }

    [Fact]
    public void Validator_forValidCommand_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new UpdateRestaurantCommand
        {
            Id = 1,
            Name = "Valid Restaurant Name",
            Description = "Valid Description",
            HasDelivery = true
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_forEmptyName_ShouldHaveValidationError()
    {
        // Arrange
        var command = new UpdateRestaurantCommand
        {
            Id = 1,
            Name = "",
            Description = "Valid Description",
            HasDelivery = true
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validator_forEmptyDescription_ShouldHaveValidationError()
    {
        // Arrange
        var command = new UpdateRestaurantCommand
        {
            Id = 1,
            Name = "Valid Restaurant Name",
            Description = "",
            HasDelivery = true
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
}