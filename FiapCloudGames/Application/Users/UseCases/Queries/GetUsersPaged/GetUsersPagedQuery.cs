namespace FiapCloudGames.Application.Users.UseCases.Queries.GetUsersPaged;

public class GetUsersPagedQuery
{
    public int Page { get; private set; }
    public int PageSize { get; private set; }

    public GetUsersPagedQuery(int page, int pageSize)
    {
        Page = page < 1 ? 1 : page;
        PageSize = pageSize < 1 ? 10 : pageSize;
        PageSize = PageSize > 100 ? 100 : PageSize;
    }
}