using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.GamePurchases.Entities;
using FiapCloudGames.Domain.GamePurchases.Ports;
using FiapCloudGames.Domain.Games.Entities;
using FiapCloudGames.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace FiapCloudGames.Infraestructure.Adapters.GamePurchases;

public class GamePurchaseQueryRepository : IGamePurchaseQueryRepository
{
    private readonly AppDbContext _dbContext;
    public GamePurchaseQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<PagedResult<GamePurchase>> GetByUserGamePurchasesAsync(int page, int pageSize, int userId, CancellationToken cancellationToken)
    {
        var totalCount = await _dbContext.GamePurchases.AsNoTracking().CountAsync(cancellationToken);

        var gamePurchase = await _dbContext.GamePurchases.AsNoTracking().Where(gp => gp.UserId == userId).OrderByDescending(gp => gp.DataGamePurchase).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return new PagedResult<GamePurchase>
        {
            Items = gamePurchase,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

    }
}
