namespace Application.Features.Auth.Users.Commands.RegisterUser;

using Abstractions.Messaging;
using Contracts.Identity;
using Domain.Abstractions;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Vms;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, AuthResponseVm>
{
    private readonly UserManager<User> _userManager;
    private readonly IAuthService _authService;
    
    public RegisterUserCommandHandler(UserManager<User> userManager, IAuthService authService)
    {
        _userManager = userManager;
        _authService = authService;
    }

    public async Task<Result<AuthResponseVm>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existeUserByEmail = await _userManager.FindByEmailAsync(request.Email!) is not null;
        
        if(existeUserByEmail)
        {
            return Result.Failure<AuthResponseVm>(UserErrors.AlreadyExistsEmail);
        }

        var existeUserByUsername = await _userManager.FindByNameAsync(request.UserName!) is not null;
        
        if(existeUserByUsername)
        {
            return Result.Failure<AuthResponseVm>(UserErrors.AlreadyExistsUsername);
        }
        
        var user = new User
        {
            Name = request.Name!,
            LastName = request.LastName!,
            PhoneNumber = request.PhoneNumber!,
            Email = request.Email,
            NormalizedEmail = request.Email!.ToUpper(),
            UserName = request.UserName,
            NormalizedUserName = request.UserName!.ToUpper(),
            EmailConfirmed = false,
            IsActive = true
        };
        
        var result = await _userManager.CreateAsync(user, request.Password!);

        if (!result.Succeeded)
            return Result.Failure<AuthResponseVm>(UserErrors.NotRegistered);

        await _userManager.AddToRoleAsync(user, request.Role!);
        var roles = await _userManager.GetRolesAsync(user);
            
        return new AuthResponseVm
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email!,
            UserName = user.UserName!,
            Token = _authService.CreateToken(user, roles),
            IsActive = user.IsActive,
            Roles = roles,
        };
    }
}