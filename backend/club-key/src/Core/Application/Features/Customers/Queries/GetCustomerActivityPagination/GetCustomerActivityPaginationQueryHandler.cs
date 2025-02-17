namespace Application.Features.Customers.Queries.GetCustomerActivityPagination;

using Abstractions.Messaging;
using AutoMapper;
using Contracts.Persistence;
using Domain.Abstractions;
using Domain.Users;
using Shared.Queries.Vms;
using Specifications.Customers;
using Vms;

public class GetCustomerActivityPaginationQueryHandler : IQueryHandler<GetCustomerActivityPaginationQuery, PaginationVm<CustomerActivityVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCustomerActivityPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Result<PaginationVm<CustomerActivityVm>>> Handle(GetCustomerActivityPaginationQuery request, CancellationToken cancellationToken)
    {
        if (request.CustomerId != null)
        {
            var user = await _unitOfWork.Repository<Customer>().GetEntityAsync(x => x.Id == request.CustomerId &&
                                                                                    x.IsActive);

            if (user is null)
            {
                return Result.Failure<PaginationVm<CustomerActivityVm>>(UserErrors.NotFound);
            }
        }
        
        var customerSpecificationParams = new CustomerActivitySpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            Sort = request.Sort,
            CustomerId = request.CustomerId,
            FromDate = request.FromDate,
            FromHour = request.ToHour,
            ToDate = request.ToDate,
            ToHour = request.ToHour,
        };
        
        var spec = new CustomerActivitySpecification(customerSpecificationParams);
        var customers = await _unitOfWork.Repository<Customer>().GetAllWithSpec(spec);
        foreach (var customer in customers)
        {
            customer.Entrances = customer.Entrances.OrderByDescending(e => e.Id).ToList();
            customer.Exits = customer.Exits.OrderByDescending(e => e.Id).ToList();
        }
        var sortedCustomers = customers
            .OrderByDescending(c => c.Entrances.FirstOrDefault()?.Id ?? 0)
            .ThenByDescending(c => c.Exits.FirstOrDefault()?.Id ?? 0)
            .ToList();
        var customerVms = _mapper.Map<List<CustomerActivityVm>>(sortedCustomers);
        
        var specCount = new CustomerActivityForCountingSpecification(customerSpecificationParams);
        var totalCustomers = await _unitOfWork.Repository<Customer>().CountAsync(specCount);
        
        var rounded = Math.Ceiling(Convert.ToDecimal(totalCustomers) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);

        var customersByPage = customers.Count;
        
        var pagination = new PaginationVm<CustomerActivityVm>
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
