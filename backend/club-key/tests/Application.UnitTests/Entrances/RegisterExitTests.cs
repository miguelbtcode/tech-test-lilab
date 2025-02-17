namespace ClubKey.Application.UnitTests.Entrances;

using System.Linq.Expressions;
using AutoMapper;
using Domain.Abstractions;
using FluentAssertions;
using global::Application.Contracts.Persistence;
using global::Application.Features.Exits.Commands.RegisterExit;
using Microsoft.EntityFrameworkCore.Storage;
using NSubstitute;

public class RegisterExitTests
{
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IMapper _mapperMock;
    private readonly RegisterExitCommandHandler _handler;

    public RegisterExitTests()
    {
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _mapperMock = Substitute.For<IMapper>();

        _handler = new RegisterExitCommandHandler(_unitOfWorkMock, _mapperMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenCustomerNotFound()
    {
        // Arrange
        var command = new RegisterExitCommand { CustomerId = 1, ExitTime = DateTime.UtcNow };

        _unitOfWorkMock.Repository<Customer>()
            .GetEntityAsync(Arg.Any<Expression<Func<Customer, bool>>>(), Arg.Any<List<Expression<Func<Customer, object>>>>(), false)
            .Returns((Customer)null!);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(CustomerErrors.NotFound);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenNoActiveEntranceExists()
    {
        // Arrange
        var command = new RegisterExitCommand { CustomerId = 1, ExitTime = DateTime.UtcNow };

        var customer = new Customer
        {
            Id = 1,
            IsActive = true,
            Entrances = new List<Entrance>()
        };

        _unitOfWorkMock.Repository<Customer>()
            .GetEntityAsync(
                Arg.Any<Expression<Func<Customer, bool>>>(), 
                Arg.Any<List<Expression<Func<Customer, object>>>>(), 
                Arg.Any<bool>()
            )
            .Returns(customer);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(EntranceErrors.NotExistsEntrance);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenLastEntranceNotFound()
    {
        // Arrange
        var command = new RegisterExitCommand { CustomerId = 1, ExitTime = DateTime.UtcNow };

        var customer = new Customer
        {
            Id = 1,
            IsActive = true,
            Entrances = new List<Entrance>
            {
                new()
                    { Id = 1, CustomerId = 1, IsActive = true, IsLastStatus = false }
            }
        };

        _unitOfWorkMock.Repository<Customer>()
            .GetEntityAsync(
                Arg.Any<Expression<Func<Customer, bool>>>(), 
                Arg.Any<List<Expression<Func<Customer, object>>>>(), 
                Arg.Any<bool>()
            )
            .Returns(customer);

        _unitOfWorkMock.Repository<Entrance>()
            .GetEntityAsync(Arg.Any<Expression<Func<Entrance, bool>>>(), Arg.Any<List<Expression<Func<Entrance, object>>>>(), false)
            .Returns((Entrance)null!);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(ExitErrors.MustBeHaveAnEntrance);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenExitIsRegistered()
    {
        // Arrange
        var command = new RegisterExitCommand { CustomerId = 1, ExitTime = DateTime.UtcNow };

        var customer = new Customer
        {
            Id = 1,
            IsActive = true,
            Entrances = new List<Entrance>
            {
                new()
                    { Id = 1, CustomerId = 1, IsActive = true, IsLastStatus = true }
            }
        };

        var lastEntrance = new Entrance { Id = 1, CustomerId = 1, IsActive = true, IsLastStatus = true };

        _unitOfWorkMock.Repository<Customer>()
            .GetEntityAsync(
                Arg.Any<Expression<Func<Customer, bool>>>(), 
                Arg.Any<List<Expression<Func<Customer, object>>>>(), 
                Arg.Any<bool>()
            )
            .Returns(customer);

        _unitOfWorkMock.Repository<Entrance>()
            .GetEntityAsync(
                Arg.Any<Expression<Func<Entrance, bool>>>(), 
                Arg.Any<List<Expression<Func<Entrance, object>>>>(),
                Arg.Any<bool>()
            )
            .Returns(lastEntrance);

        var exitEntity = new Exit { Id = 1, CustomerId = 1, ExitTime = command.ExitTime.Value, IsLastStatus = true };

        _mapperMock.Map<Exit>(command).Returns(exitEntity);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        await _unitOfWorkMock.Repository<Entrance>().Received().UpdateAsync(lastEntrance);
        await _unitOfWorkMock.Repository<Exit>().Received().AddAsync(exitEntity);
        await _unitOfWorkMock.Received().CommitTransactionAsync(Arg.Any<IDbContextTransaction>());
    }
}
