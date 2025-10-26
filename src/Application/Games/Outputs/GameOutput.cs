namespace FiapCloudGames.Application.Games.Outputs;

public record GameOutput(
    Guid PublicId,
    string Description,
    string Genre,
    DateTime ReleaseDate,
    string Developer,
    decimal Price,
    string AgeRating
);