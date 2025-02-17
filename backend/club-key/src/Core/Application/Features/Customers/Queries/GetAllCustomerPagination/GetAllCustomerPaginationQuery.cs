namespace Application.Features.Customers.Queries.GetAuditRegister;

using Abstractions.Messaging;
using Shared.Queries;
using Shared.Queries.Vms;
using Vms;

public sealed class GetAllCustomerPaginationQuery : PaginationBaseQuery, IQuery<PaginationVm<CustomerVm>>
{
    public int? CustomerId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int? Type { get; set; }
}