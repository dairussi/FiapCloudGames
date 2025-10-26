using FiapCloudGames.Domain.Common.Entities;
using FiapCloudGames.Domain.Common.ValueObjects;
using FiapCloudGames.Domain.Games.Enum;
using FiapCloudGames.Domain.Games.ValueObjects;
using FiapCloudGames.Domain.Promotions.Entities;

namespace FiapCloudGames.Domain.Games.Entities;

public class Game : BaseEntity
{

    private Game(string name, string description, GameGenreEnum genre, DateTime releaseDate, string developer, Price price, AgeRating ageRating)
    {
        Name = name;
        Description = description;
        Genre = genre;
        ReleaseDate = releaseDate;
        Developer = developer;
        Price = price;
        AgeRating = ageRating;
    }

    private Game() { }
    public Guid PublicId { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public GameGenreEnum Genre { get; private set; } = default!;
    public DateTime ReleaseDate { get; private set; }
    public string Developer { get; private set; } = default!;
    public Price Price { get; private set; } = default!;
    public AgeRating AgeRating { get; private set; } = default!;
    public ICollection<Promotion> Promotions { get; set; } = [];

    public static Game Create(string name, string description, GameGenreEnum genre, DateTime releaseDate, string developer, Price price, AgeRating ageRating)
    {
        Game game = new Game(name, description, genre, releaseDate, developer, price, ageRating);
        return game;
    }

    public void UpdateDetails(string name, string description, GameGenreEnum genre, DateTime releaseDate, string developer, Price price, AgeRating ageRating)
    {
        Name = name;
        Description = description;
        Genre = genre;
        ReleaseDate = releaseDate;
        Developer = developer;
        Price = price;
        AgeRating = ageRating;
    }

}