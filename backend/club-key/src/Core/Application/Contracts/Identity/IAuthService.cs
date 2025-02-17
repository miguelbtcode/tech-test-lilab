namespace Application.Contracts.Identity;

using Domain.Users;

public interface IAuthService
{
    string GetSessionUser();

    string CreateToken(User usuario, IList<string>? roles);
}