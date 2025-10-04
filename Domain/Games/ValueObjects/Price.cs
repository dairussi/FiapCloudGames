using System.Net.NetworkInformation;

namespace FiapCloudGames.Domain.Games.ValueObjects;

public class Price
{
    public Price(decimal value)
    {
        Value = value;
    }

    public decimal Value { get;  set; } = default!;


    public static Price Create (decimal rawInput)
    {
        if(rawInput < 0)
            throw new ArgumentException("O preço não pode ser negativo");

        //validar formatação em moeda?

        return new Price(rawInput);
    }
}
