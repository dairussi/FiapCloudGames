using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Users.Entities;
using FiapCloudGames.Domain.Users.Ports;
using FiapCloudGames.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infraestructure.Adapters.Users.Repositories;

public class UserQueryRepository : IUserQueryRepository
{
    private readonly AppDbContext _dbContext;
    public UserQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetByIdAsync(Guid publicId, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
              .AsNoTracking()
              .FirstAsync(u => u.PublicId == publicId, cancellationToken);
    }

    public async Task<PagedResult<User>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var totalCount = await _dbContext.Users.AsNoTracking().CountAsync(cancellationToken);

        var users = await _dbContext.Users
          .AsNoTracking()
          .OrderBy(u => u.CreatedAt)
          .Skip((page - 1) * pageSize)
          .Take(pageSize)
          .ToListAsync(cancellationToken);

        return new PagedResult<User>
        {
            Items = users,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}
