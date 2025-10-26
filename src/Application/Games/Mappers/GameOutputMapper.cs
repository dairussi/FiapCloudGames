using FiapCloudGames.Application.Games.Outputs;
using FiapCloudGames.Domain.Games.Entities;

namespace FiapCloudGames.Application.Games.Mappers;

public static class GameOutputMapper
{
    public static GameOutput ToOutput(this Game game)
    {
        return new GameOutput(
            game.PublicId,
            game.Description,
            game.Genre.ToString(),
            game.ReleaseDate,
            game.Developer,
            game.Price.Value,
            game.AgeRating.Rating
        );
    }

    public static List<GameOutput> ToOutput(this IEnumerable<Game> games)
    {
        return games.Select(ToOutput).ToList();
    }
}