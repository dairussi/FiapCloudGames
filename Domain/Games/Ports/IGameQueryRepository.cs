using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Games.Entities;

namespace FiapCloudGames.Domain.Games.Ports;

public interface IGameQueryRepository
{
    Task<Game> GetByIdAsync(Guid publicId, CancellationToken cancellationToken);
    Task<PagedResult<Game>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<Game> GetByIdWithPromotionsAsync(Guid publicId, CancellationToken cancellationToken);
}
