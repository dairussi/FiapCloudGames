using FiapCloudGames.Domain.Common.Entities;
using FiapCloudGames.Domain.Common.ValueObjects;
using FiapCloudGames.Domain.Games.Entities;

namespace FiapCloudGames.Domain.GamePurchases.Entities;

public class GamePurchase : BaseEntity
{
    private GamePurchase(int userId, int gameId, DateTime dataGamePurchase, Price finalPrice, Price promotionValue, int? promotionId)
    {
        UserId = userId;
        GameId = gameId;
        DataGamePurchase = dataGamePurchase;
        FinalPrice = finalPrice;
        PromotionValue = promotionValue;
        PromotionId = promotionId;
    }
    private GamePurchase() { }

    public Guid PublicId { get; private set; } = Guid.NewGuid();
    public int UserId { get; private set; }
    public int GameId { get; private set; }
    public DateTime DataGamePurchase { get; private set; }
    public Price FinalPrice { get; private set; } = default!;
    public Price? PromotionValue { get; private set; }
    public Game Game { get; private set; } = default!;
    public int? PromotionId { get; private set; }

    public static GamePurchase Create(int userId, int gameId, Price finalPrice, Price promotionValue, int? promotionId)
    {
        GamePurchase gamePurcharse = new GamePurchase(userId, gameId, DateTime.UtcNow, finalPrice, promotionValue, promotionId);
        return gamePurcharse;
    }

    internal void SetGameForOutput(Game game)
    {
        Game = game;
    }
}