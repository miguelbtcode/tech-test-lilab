namespace Application.Features.Auth.Users.Commands.UpdateUser;

using Abstractions.Messaging;
using Domain.Users;

public sealed record UpdateUserCommand(
    string Id, 
    string Name, 
    string LastName, 
    string UserName,
    string PhoneNumber, 
    string Email,
    string Role,
    string? Password) : ICommand<User>;