using FiapCloudGames.Domain.Common.ValueObjects;
using FiapCloudGames.Domain.Users.ValueObjects;

namespace FiapCloudGames.Application.Users.UseCases.Commands.AddUser;

public class AddUserInput
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string NickName { get; set; }
    public required string Password { get; set; }

    public AddUserCommand MapToCommand()
    {
        return AddUserCommand.Create(FullName.Create(Name), EmailAddress.Create(Email), NickName, RawPassword.Create(Password));
    }

}
