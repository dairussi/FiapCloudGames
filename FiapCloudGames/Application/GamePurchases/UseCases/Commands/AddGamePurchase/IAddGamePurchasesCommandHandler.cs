using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.GamePurchases.Outputs;

namespace FiapCloudGames.Application.GamePurchases.UseCases.Commands.AddGamePurchase;

public interface IAddGamePurchasesCommandHandler
{
    Task<ResultData<GamePurchaseOutput>> Handle(AddGamePurchasesComand command, CancellationToken cancellationToken);
}