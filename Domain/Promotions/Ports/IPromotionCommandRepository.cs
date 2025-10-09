using FiapCloudGames.Domain.Promotions.Entities;

namespace FiapCloudGames.Domain.Promotions.Ports;

public interface IPromotionCommandRepository
{
    Task<Promotion> AddAsync(Promotion promotion, CancellationToken cancellationToken);
    Task<Promotion> UpdateAsync(Promotion promotion, CancellationToken cancellationToken);
    Task<Promotion> GetByIdAsync(Guid publicId, CancellationToken cancellationToken);
    Task<bool> PromotionExistsAsync(Guid? publicId, CancellationToken cancellationToken);

}
