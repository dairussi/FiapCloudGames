using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Promotions.Entities;
using FiapCloudGames.Domain.Promotions.Ports;
using FiapCloudGames.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infraestructure.Adapters.Promotions.Repositories;

public class PromotionQueryRepository : IPromotionQueryRepository
{
    private readonly AppDbContext _dbContext;
    public PromotionQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Promotion> GetByIdAsync(Guid publicId, CancellationToken cancellationToken)
    {
        return await _dbContext.Promotions
            .AsNoTracking()
            .Include(p => p.Games)
            .Include(p => p.Users)
            .FirstAsync(g => g.PublicId == publicId, cancellationToken);
    }

    public async Task<PagedResult<Promotion>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var totalCount = await _dbContext.Promotions.AsNoTracking().CountAsync(cancellationToken);
        var promotions = await _dbContext.Promotions
            .AsNoTracking()
            .Include(p => p.Games)
            .Include(p => p.Users)
            .OrderBy(g => g.Description)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Promotion>
        {
            Items = promotions,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}