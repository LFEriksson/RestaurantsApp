using Application.User;
using FluentAssertions;
using Infrastructure.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Infrastructure.Tests.Authorization.Requirements;

public class MinimumAgeRequirementHandlerTests
{
    [Fact]
    public async Task HandleRequirementAsync_UserMeetsAgeRequirement_ShouldSucceed()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<MinimumAgeRequirementHandler>>();
        var mockUserContext = new Mock<IUserContext>();

        var currentUser = new CurentUser("user-123", "user@example.com", new List<string>(), new DateOnly(2000, 1, 1));
        mockUserContext.Setup(uc => uc.GetCurrentUser()).Returns(currentUser);

        var requirement = new MinimumAgeRequirement(18);
        var handler = new MinimumAgeRequirementHandler(mockLogger.Object, mockUserContext.Object);
        var context = new AuthorizationHandlerContext(new[] { requirement }, null!, null);

        // Act
        await handler.HandleAsync(context);

        // Assert
        context.HasSucceeded.Should().BeTrue();
    }

    [Fact]
    public async Task HandleRequirementAsync_UserDoesNotMeetAgeRequirement_ShouldFail()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<MinimumAgeRequirementHandler>>();
        var mockUserContext = new Mock<IUserContext>();

        var currentUser = new CurentUser("user-123", "user@example.com", new List<string>(), DateOnly.FromDateTime(DateTime.Now.AddYears(-10)));
        mockUserContext.Setup(uc => uc.GetCurrentUser()).Returns(currentUser);

        var requirement = new MinimumAgeRequirement(18);
        var handler = new MinimumAgeRequirementHandler(mockLogger.Object, mockUserContext.Object);
        var context = new AuthorizationHandlerContext(new[] { requirement }, null!, null);

        // Act
        await handler.HandleAsync(context);

        // Assert
        context.HasSucceeded.Should().BeFalse();
    }
}
