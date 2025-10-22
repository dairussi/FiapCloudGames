using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Promotions.Ports;
using FiapCloudGames.Domain.Promotions.Entities;

namespace FiapCloudGames.Application.Promotions.UseCases.Commands.AddPromotion;

public class AddOrUpdatePromotionCommandHandler : IAddOrUpdatePromotionCommandHandler
{
    private readonly IPromotionCommandRepository _promotionCommandRepository;

    public AddOrUpdatePromotionCommandHandler(IPromotionCommandRepository promotionCommandRepository)
    {
        _promotionCommandRepository = promotionCommandRepository;
    }

    public async Task<ResultData<Promotion>> Handle(AddOrUpdatePromotionCommand command, CancellationToken cancellationToken)
    {
        var promotionExists = command.PublicId is not null
            && await _promotionCommandRepository.PromotionExistsAsync(command.PublicId, cancellationToken);

        var promotion = Promotion.Create(command.Description, command.Period, command.DiscountRule);


        if (promotionExists)
            await _promotionCommandRepository.UpdateAsync(promotion, cancellationToken);
        else
            await _promotionCommandRepository.AddAsync(promotion, cancellationToken);

        return ResultData<Promotion>.Success(promotion);
    }
}
