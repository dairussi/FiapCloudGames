namespace FiapCloudGames.Application.GamePurchases.UseCases.Queries;

public class GetByUserGamePurchaseQuery
{
    public int Page { get; private set; }
    public int PageSize { get; set; }
    public GetByUserGamePurchaseQuery(int page, int pageSize)
    {
        Page = page < 1 ? 1 : page;
        PageSize = pageSize < 1 ? 10 : pageSize;
        PageSize = PageSize > 100 ? 100 : PageSize;
    }
}