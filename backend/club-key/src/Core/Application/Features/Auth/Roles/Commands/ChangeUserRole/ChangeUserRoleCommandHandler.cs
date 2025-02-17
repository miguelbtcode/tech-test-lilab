namespace Application.Features.Auth.Roles.Commands.ChangeUserRole;

using Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Roles;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

public class ChangeUserRoleCommandHandler : ICommandHandler<ChangeUserRoleCommand, bool>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ChangeUserRoleCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async Task<Result<bool>> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
    {
        var updateUser = await _userManager.FindByIdAsync(request.UserId);
        
        if(updateUser is null)
        {
            return Result.Failure<bool>(UserErrors.NotFound);
        }
        
        var currentRoles = await _userManager.GetRolesAsync(updateUser);
        await _userManager.RemoveFromRolesAsync(updateUser, currentRoles);

        var role = await _roleManager.FindByNameAsync(request.Role);

        if (role is null)
        {
            return Result.Failure<bool>(RoleErrors.NotFound);
        }
        
        var result = await _userManager.AddToRoleAsync(updateUser, role.Name!);
        return !result.Succeeded ? Result.Failure<bool>(RoleErrors.NotUpdated) : true;
    }
}