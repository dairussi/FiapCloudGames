using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Games.Outputs;

namespace FiapCloudGames.Application.Games.UseCases.Queries.GetGamesPaged;

public interface IGetGamesPagedQueryHandler
{
    Task<ResultData<PagedResult<GameOutput>>> Handle(GetGamesPagedQuery query, CancellationToken cancellationToken);
}