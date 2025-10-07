using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Users.Entities;
using FiapCloudGames.Domain.Users.Ports;
using FiapCloudGames.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Application.Users.UseCases.Queries.GetUsersPaged;

public class GetUsersPagedQueryHandler : IGetUsersPagedQueryHandler
{
    private readonly IUserQueryRepository _userQueryRepository;
    public GetUsersPagedQueryHandler(IUserQueryRepository userQueryRepository)
    {
        _userQueryRepository = userQueryRepository;
    }
    public async Task<ResultData<PagedResult<User>>> Handle(GetUsersPagedQuery query, CancellationToken cancellationToken)
    {
        var pagedResult = await _userQueryRepository.GetPagedAsync(
                query.Page,
                query.PageSize,
                cancellationToken);

        return ResultData<PagedResult<User>>.Success(pagedResult);

    }
}
