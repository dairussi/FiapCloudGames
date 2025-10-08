using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Games.Entities;

namespace FiapCloudGames.Application.Games.UseCases.Queries.GetGamesPaged;

public interface IGetGamesPagedQueryHandler
{
    Task<ResultData<PagedResult<Game>>> Handle(GetGamesPagedQuery query, CancellationToken cancellationToken);
}
