namespace Application.Features.Auth.Roles.Queries.GetAllRoles;

using Abstractions.Messaging;
using Domain.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Roles;
using Microsoft.EntityFrameworkCore;

public class GetAllRolesQueryHandler : IQueryHandler<GetAllRolesQuery, List<IdentityRole>>
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public GetAllRolesQueryHandler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Result<List<IdentityRole>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles.ToListAsync(cancellationToken);

        return roles.Any()
            ? Result.Success(roles)
            : Result.Failure<List<IdentityRole>>(RoleErrors.EmptyList);
    }
}