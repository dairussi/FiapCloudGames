using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.GamePurchases.Entities;

namespace FiapCloudGames.Application.GamePurchases.UseCases.Commands.AddGamePurchase;

public interface IAddGamePurchasesCommandHandler
{
    Task<ResultData<GamePurchase>> Handle(AddGamePurchasesComand command, CancellationToken cancellationToken);
}
