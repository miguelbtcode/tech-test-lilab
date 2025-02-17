namespace Application.Specifications.Users;

using Abstractions;
using Domain.Users;

public class UserForCountingSpecification: BaseSpecification<User>
{
    public UserForCountingSpecification(UserSpecificationParams userParams) : base(
        x => 
            (string.IsNullOrEmpty(userParams.Search) || x.Name.Contains(userParams.Search) 
                                                    || x.LastName.Contains(userParams.Search) || x.Email!.Contains(userParams.Search))
            &&
            (string.IsNullOrEmpty(userParams.Role) || x.UserRoles.Any(ur => ur.RoleId == userParams.Role)) 
    )
    {
    }
}