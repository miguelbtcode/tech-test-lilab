namespace Domain.UnitTests.Exits;

using FluentAssertions;

public class ExitTests
{
    [Fact]
    public void Exit_ShouldBeCreatedSuccessfully()
    {
        // Arrange
        var exit = ExitMock.CreateDefault();

        // Act & Assert
        exit.Should().NotBeNull();
        exit.ExitTime.Should().Be(ExitMock.DefaultExitTime);
        exit.IsLastStatus.Should().BeTrue();
        exit.CustomerId.Should().Be(ExitMock.DefaultCustomerId);
        exit.Customer.Should().NotBeNull();
    }

    [Fact]
    public void Exit_ShouldAllowUpdatingExitTime()
    {
        // Arrange
        var exit = ExitMock.CreateDefault();
        var newExitTime = DateTime.UtcNow.AddHours(1);

        // Act
        exit.ExitTime = newExitTime;

        // Assert
        exit.ExitTime.Should().Be(newExitTime);
    }
}