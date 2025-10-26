using FiapCloudGames.Application.Common.Outputs;
using FiapCloudGames.Domain.Common.ValueObjects;
using FiapCloudGames.Domain.Games.Ports;
using FiapCloudGames.Domain.Promotions.Enum;
using FiapCloudGames.Domain.Promotions.Ports;
using FiapCloudGames.Domain.Users.Ports;

namespace FiapCloudGames.Infraestructure.Adapters.Promotions.Services;

public class PromotionService : IPromotionService
{
    private readonly IGameQueryRepository _gameQueryRepository;
    private readonly IUserQueryRepository _userQueryRepository;

    public PromotionService(IGameQueryRepository gameQueryRepository, IUserQueryRepository userQueryRepository)
    {
        _gameQueryRepository = gameQueryRepository;
        _userQueryRepository = userQueryRepository;
    }

    public async Task<PromotionServiceResult> GetBestDiscountAsync(Price price, Guid gameId, int userId, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var game = await _gameQueryRepository.GetByIdWithPromotionsAsync(gameId, cancellationToken);
        var user = await _userQueryRepository.GetByIdWithPromotionsAsync(userId, cancellationToken);

        var allPromotions = game.Promotions
            .Concat(user.Promotions)
            .Where(p => p.Status == PromotionStatusEnum.Ativo && p.Period.IsActive(now))
            .ToList();

        if (!allPromotions.Any())
            return new PromotionServiceResult(0, Price.Create(0));

        var bestPromotion = allPromotions
               .Select(p => new
               {
                   Promotion = p,
                   DiscountValue = p.DiscountRule.CalculateDiscount(price.Value)
               })
               .OrderByDescending(x => x.DiscountValue)
               .First();

        return new PromotionServiceResult(bestPromotion.Promotion.Id, new Price(bestPromotion.DiscountValue));
    }
}