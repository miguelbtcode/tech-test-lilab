namespace Application.Features.Customers.Queries.GetCustomerActivityPagination;

using Abstractions.Messaging;
using Shared.Queries;
using Shared.Queries.Vms;
using Vms;

public class GetCustomerActivityPaginationQuery : PaginationBaseQuery, IQuery<PaginationVm<CustomerActivityVm>>
{
    public int? CustomerId { get; set; }
    public string? FromDate { get; set; }
    public string? FromHour { get; set; } = "00:00:00";
    public string? ToDate { get; set; }
    public string? ToHour { get; set; } = "00:00:00";
}