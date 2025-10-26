using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Games.Outputs;

namespace FiapCloudGames.Application.Games.UseCases.Commands.AddGame;

public interface IAddOrUpdateGameCommandHandler
{
    Task<ResultData<GameOutput>> Handle(AddOrUpdateGameCommand command, CancellationToken cancellationToken);
}