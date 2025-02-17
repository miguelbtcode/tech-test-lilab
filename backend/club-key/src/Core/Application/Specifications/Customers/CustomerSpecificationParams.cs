namespace Application.Specifications.Customers;

using Abstractions;
using Domain;

public class CustomerSpecificationParams : SpecificationParams
{
    public int? CustomerId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int? Type { get; set; }
}