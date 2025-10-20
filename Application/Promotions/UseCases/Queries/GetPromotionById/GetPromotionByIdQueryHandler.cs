using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Promotions.Entities;
using FiapCloudGames.Domain.Promotions.Ports;

namespace FiapCloudGames.Application.Promotions.UseCases.Queries.GetPromotionById;

public class GetPromotionByIdQueryHandler : IGetPromotionByIdQueryHandler
{
    private readonly IPromotionQueryRepository _promotionQueryRepository;
    public GetPromotionByIdQueryHandler(IPromotionQueryRepository promotionQueryRepository)
    {
        _promotionQueryRepository = promotionQueryRepository;
    }
    public async Task<ResultData<Promotion>> Handle(GetPromotionByIdQuery query, CancellationToken cancellationToken)
    {
        var promotion = await _promotionQueryRepository.GetByIdAsync(query.PublicId, cancellationToken);

        if (promotion is null)
        {
            return ResultData<Promotion>.Error("Promoção não encontrada.");
        }

        return ResultData<Promotion>.Success(promotion);

    }
}
