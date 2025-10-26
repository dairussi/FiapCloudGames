using FiapCloudGames.Domain.Promotions.Enum;
using FiapCloudGames.Domain.Promotions.ValueObjects;

namespace FiapCloudGames.Application.Promotions.UseCases.Commands.AddPromotion;

public class AddOrUpdatePromotionCommand
{
    private AddOrUpdatePromotionCommand(Guid? publicId, string description, ValidityPeriod period, DiscountRule discountRule, PromotionStatusEnum status)
    {
        PublicId = publicId;
        Description = description;
        Period = period;
        DiscountRule = discountRule;
        Status = status;
    }
    public Guid? PublicId { get; }
    public string Description { get; }
    public ValidityPeriod Period { get; }
    public DiscountRule DiscountRule { get; }
    public PromotionStatusEnum Status { get; set; }

    public static AddOrUpdatePromotionCommand Create(Guid? publicId, string description, ValidityPeriod period, DiscountRule discountRule, PromotionStatusEnum status)
    {
        return new AddOrUpdatePromotionCommand(publicId, description, period, discountRule, status);
    }
}