using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Users.Entities;

namespace FiapCloudGames.Application.Users.UseCases.Queries.GetUserById;

public interface IGetUserByIdQueryHandler
{
    Task<ResultData<User>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken);

}
