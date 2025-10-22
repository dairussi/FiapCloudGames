using FiapCloudGames.Domain.Common.Entities;
using FiapCloudGames.Domain.Games.Entities;
using FiapCloudGames.Domain.Promotions.Enum;
using FiapCloudGames.Domain.Promotions.ValueObjects;
using FiapCloudGames.Domain.Users.Entities;

namespace FiapCloudGames.Domain.Promotions.Entities;

public class Promotion : BaseEntity
{
    private Promotion(string description, ValidityPeriod period, DiscountRule discountRule)
    {
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("A descrição é obrigatória.");

        Period = period;
        DiscountRule = discountRule;
        Description = description;
        Status = period.IsActive(DateTime.UtcNow)
            ? PromotionStatusEnum.Ativo
            : PromotionStatusEnum.Agendado;
    }
    private Promotion() { }
    public ICollection<Game> Games { get; set; } = [];
    public ICollection<User> Users { get; set; } = [];
    public Guid PublicId { get; private set; } = Guid.NewGuid();
    public ValidityPeriod Period { get; private set; }
    public DiscountRule DiscountRule { get; private set; }
    public string Description { get; private set; }
    public PromotionStatusEnum Status { get; private set; }

    public static Promotion Create(string description, ValidityPeriod period, DiscountRule discountRule)
    {
        return new Promotion(description, period, discountRule);
    }

    public void Cancel()
    {
        if (Status == PromotionStatusEnum.Expirado)
        {
            throw new InvalidOperationException("Não é possível cancelar uma promoção expirada.");
        }
        Status = PromotionStatusEnum.Cancelado;
    }

    public void CheckVigency(DateTime now)
    {
        if (Status == PromotionStatusEnum.Cancelado || Status == PromotionStatusEnum.Expirado)
            return;

        if (Period.IsActive(now))
        {
            if (Status != PromotionStatusEnum.Ativo)
            {
                Status = PromotionStatusEnum.Ativo;
            }
        }
        else if (now > Period.EndDate)
        {
            Status = PromotionStatusEnum.Expirado;
        }
    }

}
