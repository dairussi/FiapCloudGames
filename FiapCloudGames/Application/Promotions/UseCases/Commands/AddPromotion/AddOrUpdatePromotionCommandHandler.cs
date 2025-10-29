using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Promotions.Mappers;
using FiapCloudGames.Application.Promotions.Outputs;
using FiapCloudGames.Domain.Promotions.Entities;
using FiapCloudGames.Domain.Promotions.Ports;
using FiapCloudGames.Domain.Games.Ports;
using FiapCloudGames.Domain.Users.Ports;
using System.Linq;

namespace FiapCloudGames.Application.Promotions.UseCases.Commands.AddPromotion;

public class AddOrUpdatePromotionCommandHandler : IAddOrUpdatePromotionCommandHandler
{
    private readonly IPromotionCommandRepository _promotionCommandRepository;
    private readonly IGameQueryRepository _gameQueryRepository;
    private readonly IUserQueryRepository _userQueryRepository;

    public AddOrUpdatePromotionCommandHandler(
        IPromotionCommandRepository promotionCommandRepository,
        IGameQueryRepository gameQueryRepository,
        IUserQueryRepository userQueryRepository)
    {
        _promotionCommandRepository = promotionCommandRepository;
        _gameQueryRepository = gameQueryRepository;
        _userQueryRepository = userQueryRepository;
    }

    public async Task<ResultData<PromotionOutput>> Handle(AddOrUpdatePromotionCommand command, CancellationToken cancellationToken)
    {
        var promotionExists = command.PublicId is not null
            && await _promotionCommandRepository.PromotionExistsAsync(command.PublicId, cancellationToken);

        var promotion = Promotion.Create(command.Description, command.Period, command.DiscountRule);

        promotion.CheckVigency(DateTime.UtcNow);

        foreach (var gamePublicId in command.GamePublicIds.Distinct())
        {
            var game = await _gameQueryRepository.GetByIdAsync(gamePublicId, cancellationToken);
            if (game is not null)
            {
                promotion.Games.Add(game);
            }
        }

        foreach (var userPublicId in command.UserPublicIds.Distinct())
        {
            var user = await _userQueryRepository.GetByIdAsync(userPublicId, cancellationToken);
            if (user is not null)
            {
                promotion.Users.Add(user);
            }
        }

        if (promotionExists)
            await _promotionCommandRepository.UpdateAsync(promotion, cancellationToken);
        else
            await _promotionCommandRepository.AddAsync(promotion, cancellationToken);

        var promotionOutput = promotion.ToOutput();

        return ResultData<PromotionOutput>.Success(promotionOutput);
    }
}