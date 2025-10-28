using FiapCloudGames.Application.GamePurchases.Outputs;
using FiapCloudGames.Domain.GamePurchases.Entities;

namespace FiapCloudGames.Application.GamePurchases.Mappers;

public static class GamePurchaseOutputMapper
{
    public static GamePurchaseOutput ToOutput(this GamePurchase gamePurchase)
    {
        return new GamePurchaseOutput(
            gamePurchase.PublicId,
            gamePurchase.Game.PublicId,
            gamePurchase.DataGamePurchase,
            gamePurchase.FinalPrice.Value,
            gamePurchase.PromotionValue?.Value
        );
    }

    public static List<GamePurchaseOutput> ToOutput(this IEnumerable<GamePurchase> purchases)
    {
        return purchases.Select(ToOutput).ToList();
    }
}