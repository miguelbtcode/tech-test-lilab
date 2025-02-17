namespace Application.Features.Users.Queries.GetAllUsersPagination;

using Abstractions.Messaging;
using Contracts.Persistence;
using Domain.Abstractions;
using Domain.Users;
using Shared.Queries.Vms;
using Specifications.Users;

public class GetAllUsersPaginationQueryHandler : IQueryHandler<GetAllUsersPaginationQuery, PaginationVm<User>>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetAllUsersPaginationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PaginationVm<User>>> Handle(GetAllUsersPaginationQuery request, CancellationToken cancellationToken)
    {
        var userSpecificationParams = new UserSpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            Sort = request.Sort,
            Role = request.Role
        };
        
        var spec = new UserSpecification(userSpecificationParams);
        var users = await _unitOfWork.Repository<User>().GetAllWithSpec(spec);
        
        var specCount = new UserForCountingSpecification(userSpecificationParams);
        var totalUsers = await _unitOfWork.Repository<User>().CountAsync(specCount);
        
        var rounded = Math.Ceiling(Convert.ToDecimal(totalUsers) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);

        var usersByPage = users.Count;

        var pagination = new PaginationVm<User>
        {
            Count = totalUsers,
            Data = users,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            ResultByPage = usersByPage
        };

        return pagination;
    }
}