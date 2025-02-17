namespace Application.Features.Auth.Users.Queries.GetUserById;

using Abstractions.Messaging;
using Vms;

public sealed record GetUserByIdQuery(string? UserId) : IQuery<AuthResponseVm>;