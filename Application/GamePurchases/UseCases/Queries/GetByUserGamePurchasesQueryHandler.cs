using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Common.Ports;
using FiapCloudGames.Domain.GamePurchases.Entities;
using FiapCloudGames.Domain.GamePurchases.Ports;

namespace FiapCloudGames.Application.GamePurchases.UseCases.Queries;

public class GetByUserGamePurchasesQueryHandler : IGetByUserGamePurchasesQueryHandler
{
    private readonly IGamePurchaseQueryRepository _gamePurchaseQueryRepository;
    private readonly IUserContext _userContext;
    public GetByUserGamePurchasesQueryHandler(IUserContext userContext, IGamePurchaseQueryRepository gamePurchaseQueryRepository)
    {
        _gamePurchaseQueryRepository = gamePurchaseQueryRepository;
        _userContext = userContext;
    }
    public async Task<ResultData<PagedResult<GamePurchase>>> Handle(GetByUserGamePurchaseQuery query, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetCurrentUserId();

        var gamePurchase = await _gamePurchaseQueryRepository.GetByUserGamePurchasesAsync(query.Page, query.PageSize, userId, cancellationToken);

        return ResultData<PagedResult<GamePurchase>>.Success(gamePurchase);
    }
}
