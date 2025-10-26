using FiapCloudGames.Application.Common;

namespace FiapCloudGames.Application.Auth.UseCases.Queries.LoginUserQuery;

public interface ILoginUserQueryHandler
{
    Task<ResultData<LoginUserQueryOutput>> Handle(LoginUserQuery query, CancellationToken cancellationToken);
}