using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Games.Entities;

namespace FiapCloudGames.Application.Games.UseCases.Queries.GetGameById;

public interface IGetGameByIdQueryHandler
{
    Task<ResultData<Game>> Handle(GetGameByIdQuery query, CancellationToken cancellationToken);
}
