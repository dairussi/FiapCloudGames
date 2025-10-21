using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Promotions.Entities;

namespace FiapCloudGames.Domain.Promotions.Ports;

public interface IPromotionQueryRepository
{
    Task<Promotion> GetByIdAsync(Guid publicId, CancellationToken cancellationToken);
    Task<PagedResult<Promotion>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken);
}
