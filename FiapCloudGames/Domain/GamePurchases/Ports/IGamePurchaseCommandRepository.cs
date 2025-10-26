using FiapCloudGames.Domain.GamePurchases.Entities;

namespace FiapCloudGames.Domain.GamePurchases.Ports;

public interface IGamePurchaseCommandRepository
{
    Task<GamePurchase> AddAsync(GamePurchase gamePurchase, CancellationToken cancellationToken);
}