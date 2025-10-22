using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Users.Entities;
using FiapCloudGames.Domain.Users.Ports;

namespace FiapCloudGames.Application.Users.UseCases.Queries.GetUserById;

public class GetUserByIdQueryHandler : IGetUserByIdQueryHandler
{
    private readonly IUserQueryRepository _userQueryRepository;
    public GetUserByIdQueryHandler(IUserQueryRepository userQueryRepository)
    {
        _userQueryRepository = userQueryRepository;
    }
    public async Task<ResultData<User>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _userQueryRepository.GetByIdAsync(query.PublicId, cancellationToken);

        if (user is null)
        {
            return ResultData<User>.Error("Usuário não encontrado.");
        }

        return ResultData<User>.Success(user);

    }
}
