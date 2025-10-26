using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Promotions.Outputs;


namespace FiapCloudGames.Application.Promotions.UseCases.Queries.GetPromotionsPaged;

public interface IGetPromotionsPagedQueryHandler
{
    Task<ResultData<PagedResult<PromotionOutput>>> Handle(GetPromotionsPagedQuery query, CancellationToken cancellationToken);
}