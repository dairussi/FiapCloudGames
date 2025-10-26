using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Users.Entities;
using FiapCloudGames.Domain.Users.ValueObjects;

namespace FiapCloudGames.Domain.Users.Ports;

public interface IUserQueryRepository
{
    Task<User> GetByIdAsync(Guid PublicId, CancellationToken cancellationToken);
    Task<PagedResult<User>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<User> GetByIdWithPromotionsAsync(int userId, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(EmailAddress email, CancellationToken cancellationToken);
}