namespace Application.Features.Auth.Users.Commands.ChangeUserPassword;

using Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

public sealed class ChangeUserPasswordCommandHandler : ICommandHandler<ChangeUserPasswordCommand, bool>
{
    private readonly UserManager<User> _userManager;

    public ChangeUserPasswordCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
    }
    
    public async Task<Result<bool>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var updateUser = await _userManager.FindByIdAsync(request.UserId);
        
        if(updateUser is null)
        {
            return Result.Failure<bool>(UserErrors.NotFound);
        }
        
        var token = await _userManager.GeneratePasswordResetTokenAsync(updateUser);
        var result = await _userManager.ResetPasswordAsync(updateUser, token, request.NewPassword);

        return !result.Succeeded ? Result.Failure<bool>(UserErrors.ResetNewError) : true;
    }
}