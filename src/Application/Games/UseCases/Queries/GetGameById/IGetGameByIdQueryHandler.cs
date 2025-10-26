using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Games.Outputs;

namespace FiapCloudGames.Application.Games.UseCases.Queries.GetGameById;

public interface IGetGameByIdQueryHandler
{
    Task<ResultData<GameOutput>> Handle(GetGameByIdQuery query, CancellationToken cancellationToken);
}