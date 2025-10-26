using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Users.Outputs;

namespace FiapCloudGames.Application.Users.UseCases.Commands.DeactivateUser;

public interface IDeactivateUserCommandHandler
{
    Task<ResultData<UserOutput>> Handle(DeactivateUserCommand command, CancellationToken cancellationToken);
}