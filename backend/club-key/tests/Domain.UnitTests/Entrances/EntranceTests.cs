namespace Domain.UnitTests.Entrances;

using FluentAssertions;

public class EntranceTests
{
    [Fact]
    public void Entrance_ShouldBeCreatedSuccessfully()
    {
        // Arrange
        var entrance = EntranceMock.CreateDefault();

        // Act & Assert
        entrance.Should().NotBeNull();
        entrance.EntranceTime.Should().Be(EntranceMock.DefaultEntranceTime);
        entrance.IsLastStatus.Should().BeTrue();
        entrance.CustomerId.Should().Be(EntranceMock.DefaultCustomerId);
        entrance.Customer.Should().NotBeNull();
    }

    [Fact]
    public void Entrance_ShouldAllowUpdatingEntranceTime()
    {
        // Arrange
        var entrance = EntranceMock.CreateDefault();
        var newEntranceTime = DateTime.UtcNow.AddHours(1);

        // Act
        entrance.EntranceTime = newEntranceTime;

        // Assert
        entrance.EntranceTime.Should().Be(newEntranceTime);
    }
}