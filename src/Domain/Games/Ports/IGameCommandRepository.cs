using FiapCloudGames.Domain.Games.Entities;

namespace FiapCloudGames.Domain.Games.Ports;

public interface IGameCommandRepository
{
    Task<Game> AddAsync(Game game, CancellationToken cancellationToken);
    Task<Game> UpdateAsync(Game game, CancellationToken cancellationToken);
    Task<bool> GameExistsAsync(Guid? publicId, string description, string developer, CancellationToken cancellationToken);
    Task<Game> GetByIdAsync(Guid publicId, CancellationToken cancellationToken);
}