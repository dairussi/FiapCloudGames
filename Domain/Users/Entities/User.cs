using FiapCloudGames.Domain.Users.ValueObjects;

namespace FiapCloudGames.Domain.Users.Entities;

public class User
{
    private User(FullName fullName, EmailAddress emailAddress, string nickName, string passwordHash, string passwordSalt)
    {
        FullName = fullName;
        Email = emailAddress;
        NickName = nickName;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }

    private User() { }
    public int Id { get; set; }
    public Guid PuplicId { get; private set; } = Guid.NewGuid();
    public FullName FullName { get; private set; } = default!;
    public EmailAddress Email { get; private set; } = default!;
    public string NickName { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public string PasswordSalt { get; private set; } = default!;

    public static User Create(FullName fullName, EmailAddress emailAddress, string nickName, string passwordHash, string passwordSalt)
    {

        User user = new User(fullName, emailAddress, nickName, passwordHash, passwordSalt);
        return user;

    }

    public void UpdatePassword(string password, string passwordIv)
    {
        PasswordHash = password;
    }
}
//TODO falta o salt e mudar o update com salt