namespace Domain.UnitTests.Entrances;

using Abstractions;
using Customers;

public static class EntranceMock
{
    public static readonly DateTime DefaultEntranceTime = DateTime.UtcNow;
    public static readonly int DefaultCustomerId = 1;
    
    public static Entrance Create(DateTime entranceTime, bool isLastStatus, int customerId, Customer customer)
    {
        return new Entrance
        {
            EntranceTime = entranceTime,
            IsLastStatus = isLastStatus,
            CustomerId = customerId,
            Customer = customer
        };
    }

    public static Entrance CreateDefault()
    {
        var customer = CustomerMock.CreateDefault();
        return Create(DefaultEntranceTime, true, DefaultCustomerId, customer);
    }
}