using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Users.Outputs;

namespace FiapCloudGames.Application.Users.UseCases.Queries.GetUserById;

public interface IGetUserByIdQueryHandler
{
    Task<ResultData<UserOutput>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken);

}