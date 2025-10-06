using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Common.Ports;
using FiapCloudGames.Domain.Users.Entities;
using FiapCloudGames.Domain.Users.Ports;

namespace FiapCloudGames.Application.Users.UseCases.Commands.AddOrUpdateUser;

public class AddOrUpdateUserCommandHandler : IAddOrUpdateUserCommandHandler
{
    private readonly IHashHelper _hashHelper;
    private readonly IUserCommandRepository _userCommandRepository;
    public AddOrUpdateUserCommandHandler(IHashHelper hashHelper, IUserCommandRepository userCommandRepository)
    {
        _hashHelper = hashHelper;
        _userCommandRepository = userCommandRepository;
    }
    public async Task<ResultData<User>> Handle(AddOrUpdateUserCommand command, CancellationToken cancellationToken)
    {
        var userExists = _userCommandRepository.GameExistsAsync(command.PublicId, cancellationToken);

        var passwordHash = _hashHelper.GenerateHash(command.Password);

        var user = User.Create(command.Name, command.Email, command.NickName, passwordHash.Hash, passwordHash.Salt);

        await _userCommandRepository.AddAsync(user, cancellationToken);

        return ResultData<User>.Success(user);

        throw new NotImplementedException();

    }
}
