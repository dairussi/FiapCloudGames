using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Users.Outputs;

namespace FiapCloudGames.Application.Users.UseCases.Commands.AddOrUpdateUser;

public interface IAddOrUpdateUserCommandHandler
{
    Task<ResultData<UserOutput>> Handle(AddOrUpdateUserCommand command, CancellationToken cancellationToken);
}