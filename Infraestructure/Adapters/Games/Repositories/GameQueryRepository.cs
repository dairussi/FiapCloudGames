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

    public async Task<bool> GameExistsAsync(Guid? publicId,string description, string developer, CancellationToken cancellationToken)
    {
        return await _dbContext.Games.AnyAsync(
            g => g.Description == description && g.Developer == developer && g.PublicId == publicId,
            cancellationToken
        );
    }

    public async Task<Game> GetByIdAsync(Guid publicId, CancellationToken cancellationToken)
    {
        return await _dbContext.Games.FirstOrDefaultAsync(g => g.PublicId == publicId, cancellationToken);
    }
}
