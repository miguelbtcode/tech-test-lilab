namespace Application.Specifications.Customers;

using Abstractions;
using Domain;
using Domain.Abstractions;

public class CustomerForCountingSpecification: BaseSpecification<Customer>
{
    public CustomerForCountingSpecification(CustomerSpecificationParams customerParams) : base(
            x => 
               //x.Id == customerParams.CustomerId &&
               // string.IsNullOrEmpty(customerParams.RegisterType) || 
               // customerParams.RegisterType == "Entrance" ? x.Entrances.Any() : x.Exits.Any())
                customerParams.Type == null ||
                x.Type == (CustomerType)customerParams.Type!)
    {
    }
}