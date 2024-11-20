using Application.User;
using Domain.Constants;
using FluentAssertions;
using Xunit;

namespace Application.Tests.Users;

public class CurentUserTests
{
    // TestMethod_Scenario_ExpectedResult
    [Theory()]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        // Arrange
        var currentUser = new CurentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], new DateOnly(1990, 1, 1));

        // Act
        var isInRole = currentUser.IsInRole(roleName);

        // Assert
        isInRole.Should().BeTrue();
    }

    [Fact()]
    public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
    {
        // Arrange
        var currentUser = new CurentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], new DateOnly(1990, 1, 1));

        // Act
        var isInRole = currentUser.IsInRole(UserRoles.Owner);

        // Assert
        isInRole.Should().BeFalse();
    }

    [Fact()]
    public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        // Arrange
        var currentUser = new CurentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], new DateOnly(1990, 1, 1));

        // Act
        var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());

        // Assert
        isInRole.Should().BeFalse();
    }
}