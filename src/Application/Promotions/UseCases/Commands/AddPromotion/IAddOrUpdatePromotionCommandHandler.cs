using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Promotions.Entities;

namespace FiapCloudGames.Application.Promotions.UseCases.Commands.AddPromotion;

public interface IAddOrUpdatePromotionCommandHandler
{
    Task<ResultData<Promotion>> Handle(AddOrUpdatePromotionCommand command, CancellationToken cancellationToken);
}
