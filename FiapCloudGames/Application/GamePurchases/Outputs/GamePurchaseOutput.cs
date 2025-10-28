namespace FiapCloudGames.Application.GamePurchases.Outputs;

public record GamePurchaseOutput(
    Guid GamePurchasePublicId,
    Guid GamePublicId,
    DateTime PurchaseDate,
    decimal FinalPrice,
    decimal? PromotionValue
);