namespace Application.Specifications.Users;

using Abstractions;
using Domain.Users;

public class UserSpecification: BaseSpecification<User>
{
    public UserSpecification(UserSpecificationParams userParams) : base(
        x => 
            (string.IsNullOrEmpty(userParams.Search) || x.Name.Contains(userParams.Search) 
                                                     || x.LastName.Contains(userParams.Search) || x.Email!.Contains(userParams.Search)
            )
            &&
            (string.IsNullOrEmpty(userParams.Role) || x.UserRoles.Any(ur => ur.RoleId == userParams.Role)) 
    )
    {
        ApplyPaging(userParams.PageSize * (userParams.PageIndex - 1), userParams.PageSize);
        AddInclude(x => x.UserRoles);

        if(!string.IsNullOrEmpty(userParams.Sort))
        {
            switch(userParams.Sort)
            {
                case "nombreAsc":
                    AddOrderBy(p => p.Name);
                    break;
                
                case "nombreDesc":
                    AddOrderByDescending(p => p.Name);
                    break;

                case "apellidoAsc":
                    AddOrderBy(p => p.LastName);
                    break;
                
                case "apellidoDesc":
                    AddOrderByDescending(p => p.LastName!);
                    break;
                
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
        }
        else {
            AddOrderByDescending(p => p.Name);
        }       
    }
}