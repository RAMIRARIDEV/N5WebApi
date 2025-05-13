using Moq;
using N5WebApi.Application.App.Permissions.Handlers.CommandHandlers;
using N5WebApi.Application.App.Permissions.Services;
using N5WebApi.Application.Abstractions;
using N5WebApi.Application.App.Permissions.Dtos;
using N5WebApi.Domain.Abstractions;
using N5WebApi.Domain.Exceptions;
using FluentAssertions;
using Permission = N5WebApi.src.Domain.Models.Permissions;
using Microsoft.Extensions.Logging;

namespace N5WebApi.Tests.Application.App.Permissions.Handlers;

public class CreatePermissionHandlerTests
{
    private readonly Mock<PermissionService> _mockPermissionService;
    private readonly Mock<IElasticService<Permission>> _mockElasticService;
    private readonly Mock<ILogger<CreatePermissionHandler>> _mockLogger;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    public CreatePermissionHandlerTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _mockPermissionService = new Mock<PermissionService>(_unitOfWork.Object);
        _mockElasticService = new Mock<IElasticService<Permission>>();
        _mockLogger = new Mock<ILogger<CreatePermissionHandler>>();
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldReturnSuccessResult()
    {
        // Arrange
        var request = new CreatePermissionRequest(
         "Ariel","Ramirez",1,DateTime.UtcNow
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

    }

    [Fact]
    public async Task Handle_CreatePermissionThrowsException_ShouldReturnFailureResult()
    {
        // Arrange
        var request = new CreatePermissionRequest(
         "Lionel", "Messi", 1, DateTime.UtcNow
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
    }
}