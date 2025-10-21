using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Promotions.Entities;
using FiapCloudGames.Domain.Promotions.Ports;

namespace FiapCloudGames.Application.Promotions.UseCases.Queries.GetPromotionsPaged;

public class GetPromotionsPagedQueryHandler : IGetPromotionsPagedQueryHandler
{
    private readonly IPromotionQueryRepository _promotionQueryRepository;
    public GetPromotionsPagedQueryHandler(IPromotionQueryRepository promotionQueryRepository)
    {
        _promotionQueryRepository = promotionQueryRepository;
    }
    public async Task<ResultData<PagedResult<Promotion>>> Handle(GetPromotionsPagedQuery query, CancellationToken cancellationToken)
    {
        var pagedResult = await _promotionQueryRepository.GetPagedAsync(
                query.Page,
                query.PageSize,
                cancellationToken);

        return ResultData<PagedResult<Promotion>>.Success(pagedResult);

    }
}
