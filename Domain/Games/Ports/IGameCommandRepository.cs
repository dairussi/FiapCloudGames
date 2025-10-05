using FiapCloudGames.Domain.Games.Entities;
using System.Runtime.CompilerServices;

namespace FiapCloudGames.Domain.Games.Ports;

public interface IGameCommandRepository
{
    Task SaveAsync(Game game, CancellationToken cancellationToken);
}
