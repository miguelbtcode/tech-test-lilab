namespace Application.Features.Auth.Users.Commands.RegisterUser;

using Abstractions.Messaging;
using Vms;

public sealed record RegisterUserCommand(
    string? Name,
    string? LastName,
    string? UserName,
    string? PhoneNumber,
    string? Email,
    string? Role,
    string? Password) : ICommand<AuthResponseVm>;