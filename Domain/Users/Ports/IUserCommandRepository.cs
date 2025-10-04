using FiapCloudGames.Domain.Users.Entities;

namespace FiapCloudGames.Domain.Users.Ports;

public interface IUserCommandRepository
{
    Task<User> AddAsync(User user, CancellationToken cancellationToken);
}
