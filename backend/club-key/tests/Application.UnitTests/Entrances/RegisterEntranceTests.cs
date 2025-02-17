namespace ClubKey.Application.UnitTests.Entrances;

using global::Application.Contracts.Persistence;
using global::Application.Features.Entrances.Commands.RegisterEntrance;

using System.Linq.Expressions;
using AutoMapper;
using Domain.Abstractions;
using FluentAssertions;
using NSubstitute;

public class RegisterEntranceTests
{
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IMapper _mapperMock;
    private readonly RegisterEntranceCommandHandler _handler;

    public RegisterEntranceTests()
    {
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _mapperMock = Substitute.For<IMapper>();
        _handler = new RegisterEntranceCommandHandler(_unitOfWorkMock, _mapperMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenEntranceIsDuplicate()
    {
        // Arrange
        var command = new RegisterEntranceCommand { CustomerId = 1, EntranceTime = DateTime.UtcNow };

        _unitOfWorkMock.Repository<Entrance>()
            .ExistsAsync(Arg.Any<Expression<Func<Entrance, bool>>>())
            .Returns(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(EntranceErrors.DuplicatedEntity, result.Error);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenCustomerNotFound()
    {
        // Arrange
        var command = new RegisterEntranceCommand { CustomerId = 1, EntranceTime = DateTime.UtcNow };

        _unitOfWorkMock.Repository<Entrance>()
            .ExistsAsync(Arg.Any<Expression<Func<Entrance, bool>>>())
            .Returns(false);

        _unitOfWorkMock.Repository<Customer>()
            .GetEntityAsync(Arg.Any<Expression<Func<Customer, bool>>>(), Arg.Any<List<Expression<Func<Customer, object>>>>(), false)
            .Returns((Customer)null!);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(CustomerErrors.NotFound, result.Error);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnMustBeHaveAnExit_WhenLastExitNotFound()
    {
        // Arrange
        var command = new RegisterEntranceCommand { 
            CustomerId = 1, 
            EntranceTime = DateTime.UtcNow 
        };

        var customer = new Customer
        {
            Id = 1,
            IsActive = true,
            Entrances = new List<Entrance>
            {
                new Entrance { 
                    Id = 1, 
                    CustomerId = 1, 
                    IsActive = true, 
                    EntranceTime = DateTime.UtcNow.AddHours(-1) 
                }
            },
            Exits = new List<Exit>()
        };

        // 1. Mock para evitar error de duplicidad
        _unitOfWorkMock.Repository<Entrance>()
            .ExistsAsync(Arg.Any<Expression<Func<Entrance, bool>>>())
            .Returns(false);

        // 2. Mock para retornar el cliente con entradas previas
        _unitOfWorkMock.Repository<Customer>()
            .GetEntityAsync(
                Arg.Any<Expression<Func<Customer, bool>>>(),
                Arg.Any<List<Expression<Func<Customer, object>>>>(),
                Arg.Any<bool>()
            )
            .Returns(customer);

        // 3. Mock para simular que no existe última salida
        _unitOfWorkMock.Repository<Exit>()
            .GetEntityAsync(
                Arg.Any<Expression<Func<Exit, bool>>>(),
                Arg.Any<List<Expression<Func<Exit, object>>>>(),
                Arg.Any<bool>()
            )
            .Returns((Exit)null!);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(EntranceErrors.MustBeHaveAnExit);
    }
}