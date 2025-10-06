using FiapCloudGames.Domain.Games.Entities;
using FiapCloudGames.Domain.Games.Ports;
using FiapCloudGames.Infraestructure.Persistence;

namespace FiapCloudGames.Infraestructure.Adapters.Games.Repositories;

public class GameCommandRepository : IGameCommandRepository
{
    private readonly AppDbContext _dbContext;

    public GameCommandRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext; 
    }

    public async Task SaveAsync(Game game, CancellationToken cancellationToken)
    {
        _dbContext.Games.Update(game);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
