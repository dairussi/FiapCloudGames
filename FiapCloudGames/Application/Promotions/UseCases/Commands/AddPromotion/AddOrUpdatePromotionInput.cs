using FiapCloudGames.Domain.Promotions.Enum;
using FiapCloudGames.Domain.Promotions.ValueObjects;

namespace FiapCloudGames.Application.Promotions.UseCases.Commands.AddPromotion;

public class AddOrUpdatePromotionInput
{
    public Guid? PublicId { get; set; }
    public required string Description { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required string DiscountType { get; set; }
    public decimal Percentage { get; set; }
    public decimal FixedAmount { get; set; }
    public required string Status { get; set; }

    public AddOrUpdatePromotionCommand MapToCommand()
    {
        if (!Enum.TryParse(DiscountType, true, out DiscountTypeEnum discountTypeEnum))
            throw new ArgumentException("Tipo de desconto inválido.", nameof(DiscountType));

        if (!Enum.TryParse(Status, true, out PromotionStatusEnum promotionStatusEnum))
            throw new ArgumentException("Status promocional inválido.", nameof(Status));

        return AddOrUpdatePromotionCommand.Create(PublicId, Description, ValidityPeriod.Create(StartDate, EndDate), DiscountRule.Create(discountTypeEnum, Percentage, FixedAmount), promotionStatusEnum);
    }
}