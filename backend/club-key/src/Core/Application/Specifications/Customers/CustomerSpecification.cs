namespace Application.Specifications.Customers;

using Abstractions;
using Domain;
using Domain.Abstractions;

public class CustomerSpecification : BaseSpecification<Customer>
{
    public CustomerSpecification(CustomerSpecificationParams customerParams) : base(
        x => 
            //x.Id == customerParams.CustomerId &&
            // string.IsNullOrEmpty(customerParams.RegisterType) || 
            // customerParams.RegisterType == "Entrance" ? x.Entrances.Any() : x.Exits.Any())
            customerParams.Type == null ||
            x.Type == (CustomerType)customerParams.Type!)
    {
        ApplyPaging(customerParams.PageSize * (customerParams.PageIndex - 1), customerParams.PageSize);

        if(!string.IsNullOrEmpty(customerParams.Sort))
        {
            switch(customerParams.Sort)
            {
                case "entranceAsc":
                    AddOrderBy(p => p.Entrances.Select(x => x.EntranceTime));
                    break;
                
                case "entranceDesc":
                    AddOrderByDescending(p => p.Entrances.Select(x => x.EntranceTime));
                    break;

                case "exitAsc":
                    AddOrderBy(p => p.Exits.Select(x => x.ExitTime));
                    break;
                
                case "exitDesc":
                    AddOrderByDescending(p => p.Exits.Select(x => x.ExitTime));
                    break;
                
                default:
                    AddOrderByDescending(p => p.Entrances.Select(x => x.EntranceTime));
                    AddOrderByDescending(p => p.Exits.Select(x => x.ExitTime));
                    break;
            }
        }
        else {
            AddOrderByDescending(p => p.CreatedBy!);
        }       
    }
}
