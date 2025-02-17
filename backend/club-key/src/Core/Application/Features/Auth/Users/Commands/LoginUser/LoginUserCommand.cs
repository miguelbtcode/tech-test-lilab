namespace Application.Features.Auth.Users.Commands.LoginUser;

using Abstractions.Messaging;
using Vms;

public sealed record LoginUserCommand(string? Email, string? Password) : ICommand<AuthResponseVm>;