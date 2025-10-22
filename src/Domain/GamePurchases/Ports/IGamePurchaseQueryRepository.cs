using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.GamePurchases.Entities;

namespace FiapCloudGames.Domain.GamePurchases.Ports;

public interface IGamePurchaseQueryRepository
{
    Task<PagedResult<GamePurchase>> GetByUserGamePurchasesAsync(int page, int pageSize, int userId, CancellationToken cancellationToken);
}
