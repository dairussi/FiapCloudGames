using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Games.Entities;
using FiapCloudGames.Domain.Games.Ports;
using FiapCloudGames.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infraestructure.Adapters.Games.Repositories;

public class GameQueryRepository : IGameQueryRepository
{
    private readonly AppDbContext _dbContext;

    public GameQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Game> GetByIdAsync(Guid publicId, CancellationToken cancellationToken)
    {
        return await _dbContext.Games
              .AsNoTracking()
              .FirstAsync(u => u.PublicId == publicId, cancellationToken);
    }

    public async Task<PagedResult<Game>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var totalCount = await _dbContext.Games.AsNoTracking().CountAsync(cancellationToken);

        var games = await _dbContext.Games.AsNoTracking().OrderBy(g => g.Description).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return new PagedResult<Game>
        {
            Items = games,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
    public async Task<Game> GetByIdWithPromotionsAsync(Guid publicId, CancellationToken cancellationToken)
    {
        return await _dbContext.Games
            .AsNoTracking()
            .Include(g => g.Promotions)
            .FirstAsync(g => g.PublicId == publicId, cancellationToken);
    }

}
