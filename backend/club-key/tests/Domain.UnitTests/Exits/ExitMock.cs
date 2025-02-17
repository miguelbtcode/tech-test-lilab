namespace Domain.UnitTests.Exits;

using Abstractions;
using Customers;

public static class ExitMock
{
    public static readonly DateTime DefaultExitTime = DateTime.UtcNow;
    public static readonly int DefaultCustomerId = 1;
    
    public static Exit Create(DateTime exitTime, bool isLastStatus, int customerId, Customer customer)
    {
        return new Exit
        {
            ExitTime = exitTime,
            IsLastStatus = isLastStatus,
            CustomerId = customerId,
            Customer = customer
        };
    }

    public static Exit CreateDefault()
    {
        var customer = CustomerMock.CreateDefault();
        return Create(DefaultExitTime, true, DefaultCustomerId, customer);
    }
}