using FiapCloudGames.Domain.Common.ValueObjects;

namespace FiapCloudGames.Application.Common.DTOs;

public class PromotionServiceResult
{
    public int? PromotionId { get; set; }
    public Price DiscountValue { get; set; }

    public PromotionServiceResult(int? promotionId, Price discountValue)
    {
        PromotionId = promotionId;
        DiscountValue = discountValue;
    }
}
