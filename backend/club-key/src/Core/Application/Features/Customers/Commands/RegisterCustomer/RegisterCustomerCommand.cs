namespace Application.Features.Customers.Commands.RegisterCustomer;

using Abstractions.Messaging;
using Domain;
using Vms;

public sealed record RegisterCustomerCommand(
    string? Name,
    string? DocumentNumber,
    CustomerType? Type,
    string? PhoneNumber,
    DateOnly? BirthDate,
    Gender? Gender,
    string? Email) : ICommand<CustomerVm>;