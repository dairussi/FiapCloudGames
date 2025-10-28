using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Promotions.Outputs;

namespace FiapCloudGames.Application.Promotions.UseCases.Queries.GetPromotionById;

public interface IGetPromotionByIdQueryHandler
{
    Task<ResultData<PromotionOutput>> Handle(GetPromotionByIdQuery query, CancellationToken cancellationToken);
}