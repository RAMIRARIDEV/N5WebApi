using Moq;
using N5WebApi.Application.App.Permissions.Handlers.CommandHandlers;
using N5WebApi.Application.App.Permissions.Services;
using N5WebApi.Application.Abstractions;
using N5WebApi.Application.App.Permissions.Dtos;
using N5WebApi.Domain.Abstractions;
using N5WebApi.Domain.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Permission = N5WebApi.src.Domain.Models.Permissions;

namespace N5WebApi.Tests.Application.App.Permissions.Handlers
{
    public class CreatePermissionHandlerTests
    {
        private readonly Mock<PermissionService> _mockPermissionService;
        private readonly Mock<IElasticService<Permission>> _mockElasticService;
        private readonly Mock<ILogger<CreatePermissionHandler>> _mockLogger;

        public CreatePermissionHandlerTests()
        {
            _mockPermissionService = new Mock<PermissionService>();
            _mockElasticService = new Mock<IElasticService<Permission>>();
            _mockLogger = new Mock<ILogger<CreatePermissionHandler>>();
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnSuccessResult()
        {
            // Arrange
            var request = new CreatePermissionRequest(
             "John","Doe",1,DateTime.UtcNow
                );

            var permission = new Permission(
                request.EmployeeForename,
                request.EmployeeSurename,
                1,
                request.PermissionDate
            );

            _mockElasticService.Setup(x => x.CreateDocument(It.IsAny<Permission>())).Verifiable();
            _mockPermissionService.Setup(x => x.CreateAsync(It.IsAny<Permission>())).ReturnsAsync(Result.Success());

            var handler = new CreatePermissionHandler(
                _mockPermissionService.Object,
                _mockElasticService.Object,
                _mockLogger.Object
            );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _mockElasticService.Verify(x => x.CreateDocument(It.IsAny<Permission>()), Times.Once);
            _mockPermissionService.Verify(x => x.CreateAsync(It.IsAny<Permission>()), Times.Once);
            _mockLogger.Verify(x => x.LogInformation(It.IsAny<string>(), It.IsAny<object>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task Handle_CreatePermissionThrowsException_ShouldReturnFailureResult()
        {
            // Arrange
            var request = new CreatePermissionRequest(
             "John", "Doe", 1, DateTime.UtcNow
                );

            var permission = new Permission(
                request.EmployeeForename,
                request.EmployeeSurename,
                -1,
                request.PermissionDate
            );

            _mockElasticService.Setup(x => x.CreateDocument(It.IsAny<Permission>())).Throws(new Exception("Elastic Error"));
            _mockPermissionService.Setup(x => x.CreateAsync(It.IsAny<Permission>())).ReturnsAsync(Result.Success());

            var handler = new CreatePermissionHandler(
                _mockPermissionService.Object,
                _mockElasticService.Object,
                _mockLogger.Object
            );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(SaveChangesErrors.SaveChangesError);
            _mockElasticService.Verify(x => x.CreateDocument(It.IsAny<Permission>()), Times.Once);
            _mockPermissionService.Verify(x => x.CreateAsync(It.IsAny<Permission>()), Times.Never);
            _mockLogger.Verify(x => x.LogError(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<object>()), Times.Once);
        }
    }
}