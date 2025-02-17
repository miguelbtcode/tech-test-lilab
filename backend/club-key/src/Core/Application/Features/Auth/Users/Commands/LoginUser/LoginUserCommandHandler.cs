namespace Application.Features.Auth.Users.Commands.LoginUser;

using Abstractions.Messaging;
using Contracts.Identity;
using Domain.Abstractions;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Vms;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, AuthResponseVm>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IAuthService _authService;
    
    public LoginUserCommandHandler(
        UserManager<User> userManager, 
        SignInManager<User> signInManager, 
        IAuthService authService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authService = authService;
    }

    public async Task<Result<AuthResponseVm>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email!);
        
        if(user is null)
        {
            return Result.Failure<AuthResponseVm>(UserErrors.NotFound);
        }

        if(!user.IsActive)
        {
            return Result.Failure<AuthResponseVm>(UserErrors.Inactive);
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password!, false);

        if(!result.Succeeded)
        {
            return Result.Failure<AuthResponseVm>(UserErrors.InvalidCredentials);
        }
        
        var roles = await _userManager.GetRolesAsync(user);

        var authResponse = new AuthResponseVm()
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email!,
            UserName = user.UserName!,
            Token = _authService.CreateToken(user, roles),
            Roles = roles           
        };

        return authResponse;
    }
}
