using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Users.Entities;

namespace FiapCloudGames.Application.Users.UseCases.Commands.DeactivateUser;

public interface IDeactivateUserCommandHandler
{
    Task<ResultData<User>> Handle(DeactivateUserCommand command, CancellationToken cancellationToken);
}
