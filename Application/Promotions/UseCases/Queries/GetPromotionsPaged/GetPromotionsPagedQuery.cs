namespace FiapCloudGames.Application.Promotions.UseCases.Queries.GetPromotionsPaged;

public class GetPromotionsPagedQuery
{
    public int Page { get; private set; }
    public int PageSize { get; private set; }

    public GetPromotionsPagedQuery(int page, int pageSize)
    {
        Page = page < 1 ? 1 : page;
        PageSize = pageSize < 1 ? 10 : pageSize;
        PageSize = PageSize > 100 ? 100 : PageSize;
    }
}
