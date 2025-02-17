namespace Application.Features.Auth.Users.Commands.UpdateUser;

using Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Roles;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, User>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    
    public UpdateUserCommandHandler(
        UserManager<User> userManager, 
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Result<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var updateUser = await _userManager.FindByIdAsync(request.Id);
        
        if(updateUser is null)
        {
            return Result.Failure<User>(UserErrors.NotFound);
        }

        updateUser.Name = request.Name;
        updateUser.LastName = request.LastName;
        updateUser.UserName = request.UserName;
        updateUser.PhoneNumber = request.PhoneNumber;
        updateUser.Email = request.Email;
        
        var resultUserManager = await _userManager.UpdateAsync(updateUser);

        return !resultUserManager.Succeeded ? Result.Failure<User>(UserErrors.NotUpdated) : updateUser;
    }
}