namespace Application.Specifications.Customers;

using System.Globalization;
using Abstractions;
using Domain.Abstractions;

public class CustomerActivitySpecification : BaseSpecification<Customer>
{
    public CustomerActivitySpecification(CustomerActivitySpecificationParams customerParams) : base(
        x =>
            (customerParams.CustomerId == null || x.Id == customerParams.CustomerId) && // Filtro por CustomerId
            (
                // Si FromDate o FromHour están vacíos, no filtramos por esta condición de fecha
                (string.IsNullOrEmpty(customerParams.FromDate) || string.IsNullOrEmpty(customerParams.FromHour) ||
                 x.Entrances.Any(e => e.EntranceTime >= 
                                      DateTime.ParseExact(
                                          string.Concat(customerParams.FromDate, " ", customerParams.FromHour), 
                                          "yyyy-MM-dd HH:mm", 
                                          CultureInfo.InvariantCulture)
                 )
                ) &&
                // Si ToDate o ToHour están vacíos, no filtramos por esta condición de fecha
                (string.IsNullOrEmpty(customerParams.ToDate) || string.IsNullOrEmpty(customerParams.ToHour) ||
                 x.Entrances.Any(e => e.EntranceTime <= 
                                      DateTime.ParseExact(
                                          string.Concat(customerParams.ToDate, " ", customerParams.ToHour), 
                                          "yyyy-MM-dd HH:mm", 
                                          CultureInfo.InvariantCulture)
                 )
                )
            )
        )
    {
        AddInclude(x => x.Entrances);
        AddInclude(x => x.Exits);
        
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
                    break;
            }
        }
        else {
            AddOrderByDescending(p => p.CreatedBy!);
        }       
    }
}