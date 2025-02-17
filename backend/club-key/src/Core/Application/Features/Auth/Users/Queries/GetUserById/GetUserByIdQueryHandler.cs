namespace Application.Features.Auth.Users.Queries.GetUserById;

using Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Vms;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, AuthResponseVm>
{
    private readonly UserManager<User> _userManager;
    
    public GetUserByIdQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<AuthResponseVm>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId!);

        if (user is null)
        {
            return Result.Failure<AuthResponseVm>(UserErrors.NotFound);
        }

        return new AuthResponseVm
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName!,
            Email = user.Email!,
            Roles = await _userManager.GetRolesAsync(user)
        };
    }
}