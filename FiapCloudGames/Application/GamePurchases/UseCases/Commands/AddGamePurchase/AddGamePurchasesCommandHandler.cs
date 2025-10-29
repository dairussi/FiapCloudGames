using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.GamePurchases.Mappers;
using FiapCloudGames.Application.GamePurchases.Outputs;
using FiapCloudGames.Domain.Common.Ports;
using FiapCloudGames.Domain.Common.ValueObjects;
using FiapCloudGames.Domain.GamePurchases.Entities;
using FiapCloudGames.Domain.GamePurchases.Ports;
using FiapCloudGames.Domain.Games.Ports;
using FiapCloudGames.Domain.Promotions.Ports;

namespace FiapCloudGames.Application.GamePurchases.UseCases.Commands.AddGamePurchase;

public class AddGamePurchasesCommandHandler : IAddGamePurchasesCommandHandler
{
    private readonly IGamePurchaseCommandRepository _gamePurchaseCommandRepository;
    private readonly IGameQueryRepository _gameQueryRepository;
    private readonly IPromotionService _promotionService;
    private readonly IUserContext _userContext;
    public AddGamePurchasesCommandHandler(
        IGamePurchaseCommandRepository gamePurchaseCommandRepository,
        IGameQueryRepository gameQueryRepository,
        IPromotionService promotionService,
        IUserContext userContext)
    {
        _gamePurchaseCommandRepository = gamePurchaseCommandRepository;
        _gameQueryRepository = gameQueryRepository;
        _promotionService = promotionService;
        _userContext = userContext;
    }
    public async Task<ResultData<GamePurchaseOutput>> Handle(AddGamePurchasesComand command, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetCurrentUserId();
        var game = await _gameQueryRepository.GetByIdAsync(command.GameId, cancellationToken);

        if (game == null)
            return ResultData<GamePurchaseOutput>.Error("Jogo não encontrado.");

        var bestPromotion = await _promotionService.GetBestDiscountAsync(game.Price, command.GameId, userId, cancellationToken);

        var finalPrice = game.Price.Value - bestPromotion.DiscountValue.Value;

        var gamePurchase = GamePurchase.Create(userId, game.Id, Price.Create(finalPrice), Price.Create(bestPromotion.DiscountValue.Value), bestPromotion.PromotionId);

        await _gamePurchaseCommandRepository.AddAsync(gamePurchase, cancellationToken);

        gamePurchase.SetGameForOutput(game);

        var gamePurchaseOutput = gamePurchase.ToOutput();

        return ResultData<GamePurchaseOutput>.Success(gamePurchaseOutput);
    }
}