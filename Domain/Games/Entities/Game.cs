using FiapCloudGames.Domain.Games.ValueObjects;
using FiapCloudGames.Domain.Games.Enum;
using System.Net.NetworkInformation;

namespace FiapCloudGames.Domain.Games.Entities;

public class Game
{

    private Game(string description, GameGenreEnum genre, DateTime releaseDate, string developer, Price price, AgeRating ageRating)
    {
        Description = description;
        Genre = genre;
        ReleaseDate = releaseDate;
        Developer = developer;
        Price = price;
        AgeRating = ageRating;
    }

    private Game(){ }
    public int Id { get; set; }
    public Guid PublicId { get; private set; } = Guid.NewGuid();
    public string Description { get; private set; } = default!;
    public GameGenreEnum Genre { get; private set; } = default!;
    public DateTime ReleaseDate { get; private set; } = default!;
    public string Developer { get; private set; } = default!;
    public Price Price { get; private set; } = default!;
    public AgeRating AgeRating { get; private set; } = default!;

    public static Game Create(string description, GameGenreEnum genre, DateTime releaseDate, string developer, Price price, AgeRating ageRating)
    {
        Game game = new Game(description, genre, releaseDate, developer, price, ageRating);
        return game;
    }

}
