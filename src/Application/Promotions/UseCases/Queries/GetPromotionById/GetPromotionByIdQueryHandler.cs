using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Promotions.Mappers;
using FiapCloudGames.Application.Promotions.Outputs;
using FiapCloudGames.Domain.Promotions.Ports;

namespace FiapCloudGames.Application.Promotions.UseCases.Queries.GetPromotionById;

public class GetPromotionByIdQueryHandler : IGetPromotionByIdQueryHandler
{
    private readonly IPromotionQueryRepository _promotionQueryRepository;
    public GetPromotionByIdQueryHandler(IPromotionQueryRepository promotionQueryRepository)
    {
        _promotionQueryRepository = promotionQueryRepository;
    }
    public async Task<ResultData<PromotionOutput>> Handle(GetPromotionByIdQuery query, CancellationToken cancellationToken)
    {
        var promotion = await _promotionQueryRepository.GetByIdAsync(query.PublicId, cancellationToken);

        if (promotion is null)
        {
            return ResultData<PromotionOutput>.Error("Promoção não encontrada.");
        }

        var promotionOutput = promotion.ToOutput();

        return ResultData<PromotionOutput>.Success(promotionOutput);

    }
}