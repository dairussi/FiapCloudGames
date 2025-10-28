using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Users.Outputs;

namespace FiapCloudGames.Application.Users.UseCases.Queries.GetUsersPaged;

public interface IGetUsersPagedQueryHandler
{
    Task<ResultData<PagedResult<UserOutput>>> Handle(GetUsersPagedQuery query, CancellationToken cancellationToken);

}