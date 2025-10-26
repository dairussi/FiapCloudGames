using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Users.Mappers;
using FiapCloudGames.Application.Users.Outputs;
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
    public async Task<ResultData<UserOutput>> Handle(AddOrUpdateUserCommand command, CancellationToken cancellationToken)
    {
        var userExists = command.PublicId is not null
        && await _userCommandRepository.UserExistsAsync(command.PublicId, cancellationToken);

        var passwordHash = _hashHelper.GenerateHash(command.Password);
        var user = User.Create(command.Name, command.Email, command.NickName, passwordHash.Hash, passwordHash.Salt, command.Role);

        if (userExists)
            await _userCommandRepository.Update(user, cancellationToken);
        else
            await _userCommandRepository.AddAsync(user, cancellationToken);

        var userOutput = user.ToOutput();

        return ResultData<UserOutput>.Success(userOutput);
    }
}