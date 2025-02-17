namespace Application.Specifications.Customers;

using Abstractions;

public class CustomerActivitySpecificationParams : SpecificationParams
{
    public int? CustomerId { get; set; }
    public string? FromDate { get; set; }
    public string? FromHour { get; set; }
    public string? ToDate { get; set; }
    public string? ToHour { get; set; }
}