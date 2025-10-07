using FiapCloudGames.Domain.Games.Entities;

namespace FiapCloudGames.Domain.Games.Ports;

public interface IGameQueryRepository
{
    Task<Game> GetByIdAsync(Guid publicId, CancellationToken cancellationToken);
}
