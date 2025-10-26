namespace FiapCloudGames.Application.Games.UseCases.Queries.GetGamesPaged;

public class GetGamesPagedQuery
{
    public int Page { get; private set; }
    public int PageSize { get; set; }
    public GetGamesPagedQuery(int page, int pageSize)
    {
        Page = page < 1 ? 1 : page;
        PageSize = pageSize < 1 ? 10 : pageSize;
        PageSize = PageSize > 100 ? 100 : PageSize;
    }
}