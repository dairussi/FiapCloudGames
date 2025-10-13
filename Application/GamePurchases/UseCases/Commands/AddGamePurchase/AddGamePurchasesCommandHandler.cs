using FiapCloudGames.Application.Common;
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
    public async Task<ResultData<GamePurchase>> Handle(AddGamePurchasesComand command, CancellationToken cancellationToken)
    {//implementar pagamento
        var userId = _userContext.GetCurrentUserId();
        var game = await _gameQueryRepository.GetByIdAsync(command.GameId, cancellationToken);
        var bestPromotion = await _promotionService.GetBestDiscountAsync(game.Price, command.GameId, userId, cancellationToken);
        var finalPrice = game.Price.Value - bestPromotion.DiscountValue.Value;

        var gamePurchase = GamePurchase.Create(userId, game.Id, Price.Create(finalPrice), Price.Create(bestPromotion.DiscountValue.Value), bestPromotion.PromotionId);

        await _gamePurchaseCommandRepository.AddAsync(gamePurchase, cancellationToken);

        return ResultData<GamePurchase>.Success(gamePurchase);
    }
}
