using FiapCloudGames.Application.Common.Outputs;
using FiapCloudGames.Domain.Common.ValueObjects;

namespace FiapCloudGames.Domain.Promotions.Ports;

public interface IPromotionService
{
    Task<PromotionServiceResult> GetBestDiscountAsync(Price price, Guid gameId, int userId, CancellationToken cancellationToken);
}