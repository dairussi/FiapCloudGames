using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Games.Entities;
using FiapCloudGames.Domain.Games.Ports;

namespace FiapCloudGames.Application.Games.UseCases.Queries.GetGamesPaged;

public class GetGamesPagedQueryHandler :IGetGamesPagedQueryHandler
{
    private readonly IGameQueryRepository _gameQueryRepository;

    public GetGamesPagedQueryHandler(IGameQueryRepository gameQueryRepository)
    {
        _gameQueryRepository = gameQueryRepository;
    }

    public async Task<ResultData<PagedResult<Game>>> Handle (GetGamesPagedQuery query, CancellationToken cancellationToken)
    {
        var pagedResult = await _gameQueryRepository.GetPagedAsync(query.Page, query.PageSize, cancellationToken);

        return ResultData<PagedResult<Game>>.Success(pagedResult);
    }

}
