using FiapCloudGames.Domain.Games.ValueObjects;
using FiapCloudGames.Domain.Games.Enum;
using System.Net.NetworkInformation;
using FiapCloudGames.Domain.Common.Entities;

namespace FiapCloudGames.Domain.Games.Entities;

public class Game : BaseEntity
{

    private Game(string description, GameGenreEnum genre, DateTime releaseDate, string developer, Price price, AgeRating ageRating, int createdBy)
    {
        Description = description;
        Genre = genre;
        ReleaseDate = releaseDate;
        Developer = developer;
        Price = price;
        AgeRating = ageRating;
        CreatedBy = createdBy;
    }

    private Game() { }
    public Guid PublicId { get; private set; } = Guid.NewGuid();
    public string Description { get; private set; } = default!;
    public GameGenreEnum Genre { get; private set; } = default!;
    public DateTime ReleaseDate { get; private set; } = default!;
    public string Developer { get; private set; } = default!;
    public Price Price { get; private set; } = default!;
    public AgeRating AgeRating { get; private set; } = default!;

    public static Game Create(string description, GameGenreEnum genre, DateTime releaseDate, string developer, Price price, AgeRating ageRating, int createdBy)
    {
        Game game = new Game(description, genre, releaseDate, developer, price, ageRating, createdBy);
        return game;
    }

    public void UpdateDetails(string description, GameGenreEnum genre, DateTime releaseDate, string developer, Price price, AgeRating ageRating)
    {

        Description = description;
        Genre = genre;
        ReleaseDate = releaseDate;
        Developer = developer;
        Price = price;
        AgeRating = ageRating;
    }

}
