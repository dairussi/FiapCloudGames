using FiapCloudGames.Domain.Promotions.Entities;
using FiapCloudGames.Domain.Promotions.Ports;
using FiapCloudGames.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infraestructure.Adapters.Promotions.Repositories;

public class PromotionCommandRepository : IPromotionCommandRepository
{
    private readonly AppDbContext _dbContext;
    public PromotionCommandRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Promotion> AddAsync(Promotion promotion, CancellationToken cancellationToken)
    {
        foreach (var game in promotion.Games)
        {
            _dbContext.Attach(game);
        }
        foreach (var user in promotion.Users)
        {
            _dbContext.Attach(user);
        }

        await _dbContext.Promotions.AddAsync(promotion, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return promotion;
    }

    public async Task<Promotion> UpdateAsync(Promotion promotion, CancellationToken cancellationToken)
    {
        foreach (var game in promotion.Games)
        {
            _dbContext.Attach(game);
        }
        foreach (var user in promotion.Users)
        {
            _dbContext.Attach(user);
        }

        _dbContext.Update(promotion);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return promotion;
    }

    public async Task<Promotion> GetByIdAsync(Guid publicId, CancellationToken cancellationToken)
    {
        try
        {
            return await _dbContext.Promotions.FirstAsync(g => g.PublicId == publicId, cancellationToken);
        }
        catch (InvalidOperationException e)
        {
            throw new KeyNotFoundException($"A promoção com o PublicId '{publicId}' não foi encontrada.", e.InnerException);
        }

    }

    public async Task<bool> PromotionExistsAsync(Guid? publicId, CancellationToken cancellationToken)
    {
        if (publicId is null)
            return false;

        return await _dbContext.Promotions.AnyAsync(u => u.PublicId == publicId, cancellationToken);
    }
}