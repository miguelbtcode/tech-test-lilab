namespace Domain.UnitTests.Users;

using Domain.Users;

public static class UserMock
{
    public static User CreateMockUser()
    {
        return new User
        {
            UserName = "johndoe",
            Name = "John",
            LastName = "Doe",
            PhoneNumber = "123456789",
            Email = "johndoe@example.com",
            IsActive = true
        };
    }
}