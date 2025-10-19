using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.GamePurchases.Entities;

namespace FiapCloudGames.Application.GamePurchases.UseCases.Queries;

public interface IGetByUserGamePurchasesQueryHandler
{
    Task<ResultData<PagedResult<GamePurchase>>> Handle(GetByUserGamePurchaseQuery query, CancellationToken cancellationToken);
}
