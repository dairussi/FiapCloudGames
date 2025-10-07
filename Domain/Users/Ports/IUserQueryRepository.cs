using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Users.Entities;

namespace FiapCloudGames.Domain.Users.Ports;

public interface IUserQueryRepository
{
    Task<User> GetByIdAsync(Guid PublicId, CancellationToken cancellationToken);
    Task<PagedResult<User>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken);
}
