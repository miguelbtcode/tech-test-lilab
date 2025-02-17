namespace ClubKey.Application.UnitTests.Exits;

using Customers;
using Domain.Abstractions;

public class ExitMock
{
    public Exit Generate()
    {
        return new Exit
        {
            ExitTime = DateTime.UtcNow.AddMinutes(-30),
            IsLastStatus = true,
            CustomerId = 1,
            Customer = new CustomerMock().Generate()
        };
    }
}