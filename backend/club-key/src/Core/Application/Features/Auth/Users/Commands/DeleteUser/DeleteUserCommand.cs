namespace Application.Features.Auth.Users.Commands.DeleteUser;

using Abstractions.Messaging;

public sealed record DeleteUserCommand(string? Id) : ICommand<bool>;