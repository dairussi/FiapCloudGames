using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Users.Mappers;
using FiapCloudGames.Application.Users.Outputs;
using FiapCloudGames.Domain.Users.Ports;

namespace FiapCloudGames.Application.Users.UseCases.Commands.DeactivateUser;

public class DeactivateUserCommandHandler : IDeactivateUserCommandHandler
{
    private readonly IUserCommandRepository _userCommandRepository;
    public DeactivateUserCommandHandler(IUserCommandRepository userCommandRepository)
    {
        _userCommandRepository = userCommandRepository;
    }
    public async Task<ResultData<UserOutput>> Handle(DeactivateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userCommandRepository.GetByIdAsync(command.PublicId, cancellationToken);

        if (user is null)
            return ResultData<UserOutput>.Error("Usuário não encontrado.");

        user.Deactivate();
        await _userCommandRepository.Update(user, cancellationToken);

        var userOutput = user.ToOutput();

        return ResultData<UserOutput>.Success(userOutput);
    }
}