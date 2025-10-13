using FiapCloudGames.Domain.Common.Enuns;
using FiapCloudGames.Domain.Users.ValueObjects;

namespace FiapCloudGames.Application.Users.UseCases.Commands.AddOrUpdateUser;

public class AddOrUpdateUserCommand
{
    private AddOrUpdateUserCommand(Guid? publicId, FullName fullName, EmailAddress email, NickName nickName, RawPassword password, EUserRole role)
    {
        PublicId = PublicId;
        Name = fullName;
        Email = email;
        NickName = nickName;
        Password = password;
        Role = role;
    }
    public Guid? PublicId { get; }
    public FullName Name { get; }
    public EmailAddress Email { get; }
    public NickName NickName { get; }
    public RawPassword Password { get; }
    public EUserRole Role { get; set; }
    public static AddOrUpdateUserCommand Create(Guid? publicId, FullName fullName, EmailAddress email, NickName nickName, RawPassword password, EUserRole role)
    {
        return new AddOrUpdateUserCommand(publicId, fullName, email, nickName, password, role);
    }
}
