using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Promotions.Entities;

namespace FiapCloudGames.Application.Promotions.UseCases.Queries.GetPromotionById;

public interface IGetPromotionByIdQueryHandler
{
    Task<ResultData<Promotion>> Handle(GetPromotionByIdQuery query, CancellationToken cancellationToken);
}
