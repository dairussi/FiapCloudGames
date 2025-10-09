using FiapCloudGames.Domain.Promotions.Enum;

namespace FiapCloudGames.Domain.Promotions.ValueObjects;

public record DiscountRule
{
    private DiscountRule(DiscountTypeEnum type, decimal percentage, decimal fixedAmount)
    {
        if (percentage < 0 || percentage > 100)
            throw new ArgumentException("O percentual de desconto deve estar entre 0 e 100.");

        if(fixedAmount < 0)
            throw new ArgumentException("O valor fixo de desconto não pode ser negativo.");

        if(type == DiscountTypeEnum.Percentual && fixedAmount > 0)
            throw new ArgumentException("Se o tipo for Percentual, o valor fixo de desconto não deve ser informado.");

        if(type == DiscountTypeEnum.Valor && percentage > 0)
            throw new ArgumentException("Se o tipo for Valor Fixo, o percentual não deve ser informado.");

        if(type == DiscountTypeEnum.Percentual && percentage <= 0)
            throw new ArgumentException("O percentual de desconto deve ser maior que zero para este tipo.");
        if(type == DiscountTypeEnum.Valor && fixedAmount <= 0)
            throw new ArgumentException("O valor fixo de desconto deve ser maior que zero para este tipo.");

        Type = type;
        Percentage = percentage;
        FixedAmount = fixedAmount;
    }

    public DiscountTypeEnum Type { get; }
    public decimal Percentage { get; }
    public decimal FixedAmount { get;}

    public static DiscountRule Create(DiscountTypeEnum type, decimal percentage, decimal fixedAmount)
    {
        return new DiscountRule(type, percentage, fixedAmount);
    }


    public decimal CalculateDiscount(decimal basePrice)
    {
        // Se o Percentual for maior que zero, usa ele. (Isso é garantido pela validação)
        if (Type == DiscountTypeEnum.Percentual)
        {
            return basePrice * (Percentage / 100);
        }
        // Caso contrário, usa o Valor Fixo.
        else
        {
            // O valor do desconto não pode ser maior que o preço base.
            return Math.Min(FixedAmount, basePrice);
        }
    }
}
