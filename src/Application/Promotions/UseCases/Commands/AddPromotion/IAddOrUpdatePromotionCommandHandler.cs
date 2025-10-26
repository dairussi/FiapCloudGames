using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Promotions.Outputs;

namespace FiapCloudGames.Application.Promotions.UseCases.Commands.AddPromotion;

public interface IAddOrUpdatePromotionCommandHandler
{
    Task<ResultData<PromotionOutput>> Handle(AddOrUpdatePromotionCommand command, CancellationToken cancellationToken);
}