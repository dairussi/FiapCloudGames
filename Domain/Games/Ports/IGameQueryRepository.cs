using FiapCloudGames.Domain.Games.Entities;

namespace FiapCloudGames.Domain.Games.Ports;

public interface IGameQueryRepository
{
    Task<bool> GameExistsAsync(Guid? publicId,string description, string developer, CancellationToken cancellationToken);
    Task<Game> GetByIdAsync(Guid publicId, CancellationToken cancellationToken);
}
