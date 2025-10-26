using FiapCloudGames.Domain.Games.Entities;
using FiapCloudGames.Domain.Games.Ports;
using FiapCloudGames.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infraestructure.Adapters.Games.Repositories;

public class GameCommandRepository : IGameCommandRepository
{
    private readonly AppDbContext _dbContext;

    public GameCommandRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Game> AddAsync(Game game, CancellationToken cancellationToken)
    {
        await _dbContext.Games.AddAsync(game, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return game;

    }

    public async Task<Game> UpdateAsync(Game game, CancellationToken cancellationToken)
    {
        _dbContext.Games.Update(game);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return game;

    }

    public async Task<bool> GameExistsAsync(Guid? publicId, string description, string developer, CancellationToken cancellationToken)
    {
        return await _dbContext.Games.AnyAsync(
            g => g.Description == description && g.Developer == developer && g.PublicId == publicId,
            cancellationToken
        );
    }

    public async Task<Game> GetByIdAsync(Guid publicId, CancellationToken cancellationToken)
    {
        try
        {
            return await _dbContext.Games.FirstAsync(g => g.PublicId == publicId, cancellationToken);
        }
        catch (InvalidOperationException e)
        {
            throw new KeyNotFoundException($"O jogo com o PublicId '{publicId}' não foi encontrado.", e.InnerException);
        }



    }
}