using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Promotions.Entities;


namespace FiapCloudGames.Application.Promotions.UseCases.Queries.GetPromotionsPaged;

public interface IGetPromotionsPagedQueryHandler
{
    Task<ResultData<PagedResult<Promotion>>> Handle(GetPromotionsPagedQuery query, CancellationToken cancellationToken);
}
