using FiapCloudGames.Domain.Users.ValueObjects;

namespace FiapCloudGames.Application.Users.UseCases.Commands.AddOrUpdateUser;

public class AddOrUpdateUserInput
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string NickName { get; set; }
    public required string Password { get; set; }

    public AddOrUpdateUserCommand MapToCommand()
    {
        return AddOrUpdateUserCommand.Create(FullName.Create(Name), EmailAddress.Create(Email), NickName, RawPassword.Create(Password));
    }

}
