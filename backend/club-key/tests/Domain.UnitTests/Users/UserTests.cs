namespace Domain.UnitTests.Users;

public class UserTests
{
    [Fact]
    public void CreateUser_ShouldHaveCorrectProperties()
    {
        // Arrange
        var user = UserMock.CreateMockUser();

        // Assert
        Assert.NotNull(user);
        Assert.Equal("johndoe", user.UserName);
        Assert.Equal("John", user.Name);
        Assert.Equal("Doe", user.LastName);
        Assert.Equal("123456789", user.PhoneNumber);
        Assert.Equal("johndoe@example.com", user.Email);
        Assert.True(user.IsActive);
    }

    [Fact]
    public void DeactivateUser_ShouldSetIsActiveToFalse()
    {
        // Arrange
        var user = UserMock.CreateMockUser();

        // Act
        user.IsActive = false;

        // Assert
        Assert.False(user.IsActive);
    }
}