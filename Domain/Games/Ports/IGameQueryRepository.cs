namespace FiapCloudGames.Domain.Games.Ports;

public interface IGameQueryRepository
{
    Task<bool> GameExistsAsync(string description, string developer, CancellationToken cancellationToken);
}
