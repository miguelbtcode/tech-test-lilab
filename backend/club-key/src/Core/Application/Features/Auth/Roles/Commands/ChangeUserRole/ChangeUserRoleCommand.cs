namespace Application.Features.Auth.Roles.Commands.ChangeUserRole;

using Abstractions.Messaging;

public sealed record ChangeUserRoleCommand(
    string UserId,
    string Role) : ICommand<bool>;
