using FiapCloudGames.Domain.Users.Entities;

namespace FiapCloudGames.Domain.Users.Ports;

public interface IUserQueryRepository
{
    Task<User> GetByIdAsync(Guid PublicId, CancellationToken cancellationToken);

}
