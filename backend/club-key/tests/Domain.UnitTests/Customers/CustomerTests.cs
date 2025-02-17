namespace Domain.UnitTests.Customers;

using FluentAssertions;

public class CustomerTests
{
    [Fact]
    public void Create_ShouldReturnCustomer_WhenValidDataProvided()
    {
        // Act
        var customer = CustomerMock.CreateDefault();

        // Assert
        customer.Should().NotBeNull();
        customer.Name.Should().Be("John Doe");
        customer.Type.Should().Be(CustomerType.Member);
        customer.PhoneNumber.Should().Be("123456789");
        customer.BirthDate.Should().Be(new DateOnly(1990, 5, 20));
        customer.Gender.Should().Be(Gender.Male);
        customer.Email.Should().Be("johndoe@example.com");
        customer.Entrances.Should().BeEmpty();
        customer.Exits.Should().BeEmpty();
    }
    
    [Theory]
    [InlineData(2000, 1, 1)]
    [InlineData(2010, 12, 31)]
    [InlineData(1980, 6, 15)]
    public void CalculateAge_ShouldReturnCorrectAge(int year, int month, int day)
    {
        // Arrange
        var birthDate = new DateOnly(year, month, day);
        var today = DateOnly.FromDateTime(DateTime.Today);
        var expectedAge = today.Year - year;
        if (today < birthDate.AddYears(expectedAge)) expectedAge--;

        // Act
        var customer = CustomerMock.CreateWithCustomData("Test", "123456781", CustomerType.Member, "123456789", birthDate, Gender.Male, "test@example.com");
        var calculatedAge = customer.GetType().GetProperty("Age", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(customer);

        // Assert
        calculatedAge.Should().Be(expectedAge);
    }
    
    [Fact]
    public void Create_ShouldInitializeEntrancesAndExitsAsEmpty()
    {
        // Act
        var customer = CustomerMock.CreateDefault();

        // Assert
        customer.Entrances.Should().NotBeNull().And.BeEmpty();
        customer.Exits.Should().NotBeNull().And.BeEmpty();
    }
}