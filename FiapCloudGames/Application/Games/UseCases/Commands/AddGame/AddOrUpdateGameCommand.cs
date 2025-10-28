using FiapCloudGames.Domain.Common.ValueObjects;
using FiapCloudGames.Domain.Games.Enum;
using FiapCloudGames.Domain.Games.ValueObjects;

namespace FiapCloudGames.Application.Games.UseCases.Commands.AddGame;

public class AddOrUpdateGameCommand
{
    private AddOrUpdateGameCommand(string name, string description, GameGenreEnum genre, DateTime releaseDate, string developer, Price price, AgeRating ageRating, Guid? publicId)
    {
        Name = name;
        Description = description;
        Genre = genre;
        ReleaseDate = releaseDate;
        Developer = developer;
        Price = price;
        AgeRating = ageRating;
        PublicId = publicId;
    }

    public string Name { get; }
    public string Description { get; }
    public GameGenreEnum Genre { get; }
    public DateTime ReleaseDate { get; }
    public string Developer { get; }
    public Price Price { get; }
    public AgeRating AgeRating { get; }
    public Guid? PublicId { get; }

    public static AddOrUpdateGameCommand Create(string name, string description, GameGenreEnum genre, DateTime releaseDate, string developer, Price price, AgeRating ageRating, Guid? publicId)
    {
        return new AddOrUpdateGameCommand(name, description, genre, releaseDate, developer, price, ageRating, publicId);
    }
}