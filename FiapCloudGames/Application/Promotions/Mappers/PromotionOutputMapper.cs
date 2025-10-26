using FiapCloudGames.Application.Promotions.Outputs;
using FiapCloudGames.Domain.Promotions.Entities;
using FiapCloudGames.Domain.Promotions.Enum;

namespace FiapCloudGames.Application.Promotions.Mappers;

public static class PromotionOutputMapper
{
    public static PromotionOutput ToOutput(this Promotion promotion)
    {
        var discountValue = promotion.DiscountRule.Type == DiscountTypeEnum.Percentual
            ? promotion.DiscountRule.Percentage
            : promotion.DiscountRule.FixedAmount;

        return new PromotionOutput(
            promotion.PublicId,
            promotion.Description,
            promotion.Period.StartDate,
            promotion.Period.EndDate,
            promotion.Status.ToString(),
            discountValue,
            promotion.DiscountRule.Type.ToString()
        );
    }

    public static List<PromotionOutput> ToOutput(this IEnumerable<Promotion> promotion)
    {
        return promotion.Select(ToOutput).ToList();
    }
}