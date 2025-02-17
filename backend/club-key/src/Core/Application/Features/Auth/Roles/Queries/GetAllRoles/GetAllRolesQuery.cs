namespace Application.Features.Auth.Roles.Queries.GetAllRoles;

using Abstractions.Messaging;
using Microsoft.AspNetCore.Identity;

public sealed record GetAllRolesQuery : IQuery<List<IdentityRole>>;