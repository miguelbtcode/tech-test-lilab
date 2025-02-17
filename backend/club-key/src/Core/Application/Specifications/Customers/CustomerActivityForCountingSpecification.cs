namespace Application.Specifications.Customers;

using System.Globalization;
using Abstractions;
using Domain.Abstractions;

public class CustomerActivityForCountingSpecification : BaseSpecification<Customer>
{
    public CustomerActivityForCountingSpecification(CustomerActivitySpecificationParams customerActivityParams) : base(
        x =>
            (customerActivityParams.CustomerId == null || x.Id == customerActivityParams.CustomerId) && // Filtro por CustomerId
            (
                // Si FromDate o FromHour están vacíos, no filtramos por esta condición de fecha
                (string.IsNullOrEmpty(customerActivityParams.FromDate) || string.IsNullOrEmpty(customerActivityParams.FromHour) ||
                 x.Entrances.Any(e => e.EntranceTime >= 
                                      DateTime.ParseExact(
                                          string.Concat(customerActivityParams.FromDate, " ", customerActivityParams.FromHour), 
                                          "yyyy-MM-dd HH:mm", 
                                          CultureInfo.InvariantCulture)
                 )
                ) &&
                // Si ToDate o ToHour están vacíos, no filtramos por esta condición de fecha
                (string.IsNullOrEmpty(customerActivityParams.ToDate) || string.IsNullOrEmpty(customerActivityParams.ToHour) ||
                 x.Entrances.Any(e => e.EntranceTime <= 
                                      DateTime.ParseExact(
                                          string.Concat(customerActivityParams.ToDate, " ", customerActivityParams.ToHour), 
                                          "yyyy-MM-dd HH:mm", 
                                          CultureInfo.InvariantCulture)
                 )
                )
            )
    )
    {
    }
}