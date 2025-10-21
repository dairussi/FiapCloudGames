using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Games.Entities;

namespace FiapCloudGames.Application.Games.UseCases.Commands.AddGame;

public interface IAddOrUpdateGameCommandHandler
{
    Task<ResultData<Game>> Handle(AddOrUpdateGameCommand command, CancellationToken cancellationToken);
}
