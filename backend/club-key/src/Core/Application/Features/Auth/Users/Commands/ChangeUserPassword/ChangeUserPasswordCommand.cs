namespace Application.Features.Auth.Users.Commands.ChangeUserPassword;

using Abstractions.Messaging;

public sealed record ChangeUserPasswordCommand(
    string UserId,
    string NewPassword) : ICommand<bool>;