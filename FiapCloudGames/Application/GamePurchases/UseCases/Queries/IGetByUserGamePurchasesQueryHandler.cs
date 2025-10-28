using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.GamePurchases.Outputs;

namespace FiapCloudGames.Application.GamePurchases.UseCases.Queries;

public interface IGetByUserGamePurchasesQueryHandler
{
    Task<ResultData<PagedResult<GamePurchaseOutput>>> Handle(GetByUserGamePurchaseQuery query, CancellationToken cancellationToken);
}