using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Users.Entities;

namespace FiapCloudGames.Application.Users.UseCases.Commands.AddOrUpdateUser;

public interface IAddOrUpdateUserCommandHandler
{
    Task<ResultData<User>> Handle(AddOrUpdateUserCommand command, CancellationToken cancellationToken);
}
