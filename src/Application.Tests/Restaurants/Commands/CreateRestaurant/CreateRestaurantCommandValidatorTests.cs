using Application.Restaurants.Commands.CreateRestaurantCommand;
using FluentValidation.TestHelper;
using Xunit;

namespace Application.Tests.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidatorTests
{
    private readonly CreateRestaurantCommandValidator _validator;

    public CreateRestaurantCommandValidatorTests()
    {
        _validator = new CreateRestaurantCommandValidator();
    }

    [Fact]
    public void Validator_forValidCommand_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new CreateRestaurantCommand
        {
            Name = "Valid Restaurant Name",
            Description = "Valid Description",
            Category = "Valid Category",
            StreetAddress = "123 Valid Street",
            City = "Valid City",
            ZipCode = "12345",
            ContactEmail = "valid@example.com",
            ContactNumber = "+1234567890",
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
        var command = new CreateRestaurantCommand
        {
            Name = "",
            Description = "Valid Description",
            Category = "Valid Category",
            StreetAddress = "123 Valid Street",
            City = "Valid City",
            ZipCode = "12345",
            ContactEmail = "valid@example.com",
            ContactNumber = "+1234567890",
            HasDelivery = true
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validator_forInvalidEmail_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateRestaurantCommand
        {
            Name = "Valid Restaurant Name",
            Description = "Valid Description",
            Category = "Valid Category",
            StreetAddress = "123 Valid Street",
            City = "Valid City",
            ZipCode = "12345",
            ContactEmail = "invalid-email",
            ContactNumber = "+1234567890",
            HasDelivery = true
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
    }

    [Fact]
    public void Validator_forInvalidPhoneNumber_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateRestaurantCommand
        {
            Name = "Valid Restaurant Name",
            Description = "Valid Description",
            Category = "Valid Category",
            StreetAddress = "123 Valid Street",
            City = "Valid City",
            ZipCode = "12345",
            ContactEmail = "valid@example.com",
            ContactNumber = "invalid-phone-number",
            HasDelivery = true
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ContactNumber);
    }
}