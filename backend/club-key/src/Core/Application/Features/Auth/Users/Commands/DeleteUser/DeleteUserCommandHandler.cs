namespace Application.Features.Auth.Users.Commands.DeleteUser;

using Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Users;
using Exceptions;
using Microsoft.AspNetCore.Identity;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, bool>
{
    private readonly UserManager<User> _userManager;
    
    public DeleteUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id!);
        
        if (user is null)
        {
            return Result.Failure<bool>(UserErrors.NotFound);
        }

        user.IsActive = false;
        
        var resultDelete = await _userManager.UpdateAsync(user);

        return !resultDelete.Succeeded ? Result.Failure<bool>(UserErrors.NotDeleted) : true;
    }
}