namespace FiapCloudGames.Application.GamePurchases.UseCases.Commands.AddGamePurchase;

public class AddGamePurchasesComand
{
    public AddGamePurchasesComand(Guid gameId)
    {
        GameId = gameId;
    }

    public Guid GameId { get; set; }
}
