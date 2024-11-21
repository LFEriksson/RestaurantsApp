using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace API.Middlewares.Tests
{
    public class ErrorHandlingMiddlewareTests
    {
        [Fact]
        public async Task InvokeAsync_WhenNoExceptionThrown_ShouldCallNextDelegate()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var httpContext = new DefaultHttpContext();
            var nextDelegateMock = new Mock<RequestDelegate>();

            // Act
            await middleware.InvokeAsync(httpContext, nextDelegateMock.Object);

            // Assert
            nextDelegateMock.Verify(next => next.Invoke(httpContext), Times.Once);
        }

        [Fact]
        public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldReturn404()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var notFoundException = new NotFoundException(nameof(Restaurant), "1");

            // Act
            await middleware.InvokeAsync(httpContext, _ => throw notFoundException);

            // Assert
            Xunit.Assert.Equal(404, httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task InvokeAsync_WhenNotAuthorizedExceptionThrown_ShouldReturn403()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var httpContext = new DefaultHttpContext();
            var nextDelegateMock = new Mock<RequestDelegate>();
            nextDelegateMock.Setup(next => next.Invoke(It.IsAny<HttpContext>())).ThrowsAsync(new NotAuthorizedException());

            // Act
            await middleware.InvokeAsync(httpContext, nextDelegateMock.Object);

            // Assert
            Xunit.Assert.Equal(403, httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task InvokeAsync_WhenGenericExceptionThrown_ShouldReturn500()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var httpContext = new DefaultHttpContext();
            var nextDelegateMock = new Mock<RequestDelegate>();
            nextDelegateMock.Setup(next => next.Invoke(It.IsAny<HttpContext>())).ThrowsAsync(new Exception("Unexpected error"));

            // Act
            await middleware.InvokeAsync(httpContext, nextDelegateMock.Object);

            // Assert
            Xunit.Assert.Equal(500, httpContext.Response.StatusCode);
        }
    }
}