namespace FiapCloudGames.Domain.Games.ValueObjects;

public class AgeRating
{
    public AgeRating(string rating, int minimiumAge)
    {
        Rating = rating;
        MinimiumAge = minimiumAge;
    }

    public string Rating { get; private set; } = default!;
    public int MinimiumAge { get; private set; } = default!;

    public static AgeRating Create(string rating)
    {
        if (string.IsNullOrWhiteSpace(rating))
            throw new ArgumentException("Classificação etária inválida.");

        switch (rating.ToUpper())
        {
            case "LIVRE":
            case "FREE":
                return new AgeRating("Livre", 0);
            case "10+":
                return new AgeRating("10+", 10);
            case "12+":
                return new AgeRating("12+", 12);
            case "14+":
                return new AgeRating("14+", 14);
            case "16+":
                return new AgeRating("16+", 16);
            case "18+":
                return new AgeRating("18+", 18);
            default:
                throw new ArgumentException("Classificação etária inválida.");
        }
    }
}