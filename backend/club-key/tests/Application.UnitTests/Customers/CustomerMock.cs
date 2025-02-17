namespace ClubKey.Application.UnitTests.Customers;

using Domain;
using Domain.Abstractions;

public class CustomerMock
{
    public Customer Generate()
    {
        return Customer.Create(
            name: "John Doe",
            documentNumber: "123456781",
            type: CustomerType.Member,
            phoneNumber: "123456789",
            birthDate: new DateOnly(1990, 5, 20),
            gender: Gender.Male,
            email: "johndoe@example.com"
        );
    }
}