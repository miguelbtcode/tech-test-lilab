namespace Application.Features.Customers.Queries.GetAuditRegister;

using Abstractions.Messaging;
using AutoMapper;
using Contracts.Persistence;
using Domain.Abstractions;
using Shared.Queries.Vms;
using Specifications.Customers;
using Vms;

public class GetAllCustomerPaginationQueryHandler : IQueryHandler<GetAllCustomerPaginationQuery, PaginationVm<CustomerVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public GetAllCustomerPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<PaginationVm<CustomerVm>>> Handle(GetAllCustomerPaginationQuery request, CancellationToken cancellationToken)
    {
        var customerSpecificationParams = new CustomerSpecificationParams
        {
            CustomerId = request.CustomerId,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            Sort = request.Sort,
            FromDate = request.FromDate,
            ToDate = request.ToDate,
            Type = request.Type,
        };
        
        var spec = new CustomerSpecification(customerSpecificationParams);
        var customers = await _unitOfWork.Repository<Customer>().GetAllWithSpec(spec);
        var customerVms = _mapper.Map<List<CustomerVm>>(customers);
        
        var specCount = new CustomerForCountingSpecification(customerSpecificationParams);
        var totalCustomers = await _unitOfWork.Repository<Customer>().CountAsync(specCount);
        
        var rounded = Math.Ceiling(Convert.ToDecimal(totalCustomers) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);

        var customersByPage = customers.Count;

        var pagination = new PaginationVm<CustomerVm>
        {
            Count = totalCustomers,
            Data = customerVms,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            ResultByPage = customersByPage
        };

        return pagination;
    }
}
