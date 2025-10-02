using FiapCloudGames.Domain.Users.ValueObjects;

namespace FiapCloudGames.Application.Users.UseCases.Commands.AddUser;

public class AddUserCommand
{
    private AddUserCommand(FullName fullName, EmailAddress email, string nickName, RawPassword password)
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
    public static AddUserCommand Create(FullName fullName, EmailAddress email, string nickName, RawPassword password)
    {
        return new AddUserCommand(fullName, email, nickName, password);
    }
}
