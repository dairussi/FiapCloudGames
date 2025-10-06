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
}
