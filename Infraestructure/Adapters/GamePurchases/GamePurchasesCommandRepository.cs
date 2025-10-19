using FiapCloudGames.Domain.GamePurchases.Entities;
using FiapCloudGames.Domain.GamePurchases.Ports;
using FiapCloudGames.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infraestructure.Adapters.GamePurchases;

public class GamePurchasesCommandRepository : IGamePurchaseCommandRepository
{
    private readonly AppDbContext _dbContext;
    public GamePurchasesCommandRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GamePurchase> AddAsync(GamePurchase gamePurchase, CancellationToken cancellationToken)
    {
        await _dbContext.GamePurchases.AddAsync(gamePurchase, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return gamePurchase;
    }

}