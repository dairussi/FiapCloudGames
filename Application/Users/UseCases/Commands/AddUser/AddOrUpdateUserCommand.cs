using FiapCloudGames.Domain.Users.ValueObjects;

namespace FiapCloudGames.Application.Users.UseCases.Commands.AddOrUpdateUser;

public class AddOrUpdateUserCommand
{
    private AddOrUpdateUserCommand(FullName fullName, EmailAddress email, string nickName, RawPassword password)
    {
        Name = fullName;
        Email = email;
        NickName = nickName;
        Password = password;
    }
    public FullName Name { get; }
    public EmailAddress Email { get; }
    public string NickName { get; }
    public RawPassword Password { get; }
    public static AddOrUpdateUserCommand Create(FullName fullName, EmailAddress email, string nickName, RawPassword password)
    {
        return new AddOrUpdateUserCommand(fullName, email, nickName, password);
    }
}
