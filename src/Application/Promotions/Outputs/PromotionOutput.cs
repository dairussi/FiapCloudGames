namespace FiapCloudGames.Application.Promotions.Outputs;

public record PromotionOutput(
    Guid PublicId,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    string Status,
    decimal DiscountValue,
    string DiscountType
);