using FiapCloudGames.Domain.Games.Enum;
using FiapCloudGames.Domain.Games.ValueObjects;

namespace FiapCloudGames.Application.Games.UseCases.Commands.AddGame;

public class AddOrUpdateGameCommand
{
    private AddOrUpdateGameCommand(Guid? publicId,string description, GameGenreEnum genre, DateTime releaseDate, string developer, Price price, AgeRating ageRating)
    {
        PublicId = publicId;
        Description = description;
        Genre = genre;
        ReleaseDate = releaseDate;
        Developer = developer;
        Price = price;
        AgeRating = ageRating;
    }

    public Guid? PublicId { get;}
    public string Description { get; } 
    public GameGenreEnum Genre { get; } 
    public DateTime ReleaseDate { get; } 
    public string Developer { get; } 
    public Price Price { get; } 
    public AgeRating AgeRating { get; }

    public static AddOrUpdateGameCommand Create(Guid? publicId, string description, GameGenreEnum genre, DateTime releaseDate, string developer, Price price, AgeRating ageRating)
    {
        return new AddOrUpdateGameCommand(publicId,description, genre, releaseDate, developer, price, ageRating);
    }
}
