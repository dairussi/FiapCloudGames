using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Common.Ports;
using FiapCloudGames.Domain.Users.Entities;

namespace FiapCloudGames.Application.Users.UseCases.Commands.AddOrUpdateUser;

public class AddOrUpdateUserCommandHandler : IAddOrUpdateUserCommandHandler
{
    private readonly IHashHelper _hashHelper;
    public AddOrUpdateUserCommandHandler(IHashHelper hashHelper)
    {
        _hashHelper = hashHelper;
    }
    public Task<ResultData<User>> Handle(AddOrUpdateUserCommand command, CancellationToken cancellationToken)
    {
        var passwordHash = _hashHelper.GenerateHash(command.Password);

        var user = User.Create(command.Name, command.Email, command.NickName, passwordHash.Hash, passwordHash.Salt);


    }
}
