namespace Application.Features.Users.Queries.GetAllUsersPagination;

using Abstractions.Messaging;
using Domain.Users;
using Shared.Queries;
using Shared.Queries.Vms;

public sealed class GetAllUsersPaginationQuery : PaginationBaseQuery, IQuery<PaginationVm<User>>
{
    public string? Role { get; set; }
}