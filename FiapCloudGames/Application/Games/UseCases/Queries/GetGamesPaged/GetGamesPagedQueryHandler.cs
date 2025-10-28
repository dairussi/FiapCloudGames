using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Games.Mappers;
using FiapCloudGames.Application.Games.Outputs;
using FiapCloudGames.Domain.Games.Ports;

namespace FiapCloudGames.Application.Games.UseCases.Queries.GetGamesPaged;

public class GetGamesPagedQueryHandler : IGetGamesPagedQueryHandler
{
    private readonly IGameQueryRepository _gameQueryRepository;

    public GetGamesPagedQueryHandler(IGameQueryRepository gameQueryRepository)
    {
        _gameQueryRepository = gameQueryRepository;
    }

    public async Task<ResultData<PagedResult<GameOutput>>> Handle(GetGamesPagedQuery query, CancellationToken cancellationToken)
    {
        var pagedResult = await _gameQueryRepository.GetPagedAsync(query.Page, query.PageSize, cancellationToken);

        var items = pagedResult.Items.ToOutput();

        var pagedResultGameOutput = new PagedResult<GameOutput>
        {
            Items = items,
            Page = pagedResult.Page,
            PageSize = pagedResult.PageSize,
            TotalCount = pagedResult.TotalCount
        };

        return ResultData<PagedResult<GameOutput>>.Success(pagedResultGameOutput);
    }

}