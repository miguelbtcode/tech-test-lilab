namespace Domain.UnitTests.Customers;

using Abstractions;

public static class CustomerMock
{
    public static Customer CreateDefault()
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

    public static Customer CreateWithCustomData(
        string name, 
        string documentNumber,
        CustomerType type, 
        string phoneNumber, 
        DateOnly? birthDate, 
        Gender gender, 
        string email)
    {
        return Customer.Create(name, documentNumber, type, phoneNumber, birthDate, gender, email);
    }
}