using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Users.Entities;

namespace FiapCloudGames.Application.Users.UseCases.Queries.GetUsersPaged;

public interface IGetUsersPagedQueryHandler
{
    Task<ResultData<PagedResult<User>>> Handle(GetUsersPagedQuery query, CancellationToken cancellationToken);

}