using FiapCloudGames.Domain.Common.Enuns;
using FiapCloudGames.Domain.Users.ValueObjects;

namespace FiapCloudGames.Application.Users.UseCases.Commands.AddOrUpdateUser;

public class AddOrUpdateUserInput
{
    public Guid? PublicId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Nick { get; set; }
    public required string Password { get; set; }
    public required int Role { get; set; }

    public AddOrUpdateUserCommand MapToCommand()
    {
        return AddOrUpdateUserCommand.Create(PublicId, FullName.Create(Name), EmailAddress.Create(Email), NickName.Create(Nick), RawPassword.Create(Password), (EUserRole)Role);
    }

}