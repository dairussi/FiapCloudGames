using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Games.Entities;
using FiapCloudGames.Domain.Games.Ports;
using FiapCloudGames.Domain.Users.Entities;

namespace FiapCloudGames.Application.Games.UseCases.Queries.GetGameById;

public class GetGameByIdQueryHandler : IGetGameByIdQueryHandler
{
    private readonly IGameQueryRepository _gameQueryRepository;
    public GetGameByIdQueryHandler(IGameQueryRepository gameQueryRepository)
    {
        _gameQueryRepository = gameQueryRepository;
    }

    public async Task<ResultData<Game>> Handle (GetGameByIdQuery query, CancellationToken cancellationToken)
    {
        var game = await _gameQueryRepository.GetByIdAsync(query.PublicId, cancellationToken);

        if(game is null)
        {
            return ResultData<Game>.Error("Jogo não encontrado.");
        }

        return ResultData<Game>.Success(game);
    }
}
