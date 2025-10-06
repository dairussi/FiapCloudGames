using FiapCloudGames.Domain.Games.Enum;
using FiapCloudGames.Domain.Games.ValueObjects;

namespace FiapCloudGames.Application.Games.UseCases.Commands.AddGame;

public class AddOrUpdateGameInput
{
    public Guid? PublicId { get; set; }
    public required string Description { get; set; }
    public required string Genre { get; set; }
    public required DateTime ReleaseDate { get; set; }
    public required string Developer { get; set; }
    public required decimal PriceValue { get; set; }
    public required string AgeRatingValue { get; set; }
    public required int CreatedBy { get; set; }

    public AddOrUpdateGameCommand MapToCommand()
    {
        if (!Enum.TryParse(Genre, true, out GameGenreEnum genreEnum))
        {
            throw new ArgumentException("Gênero do jogo inválido.", nameof(Genre));
        }

        return AddOrUpdateGameCommand.Create(PublicId, Description, genreEnum, ReleaseDate, Developer, Price.Create(PriceValue), AgeRating.Create(AgeRatingValue), CreatedBy);
    }
}
