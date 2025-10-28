using FiapCloudGames.Domain.Users.ValueObjects;

namespace FiapCloudGames.Application.Auth.UseCases.Queries.LoginUserQuery;

public class LoginUserQuery
{
    public LoginUserQuery(EmailAddress emailAdress, RawPassword rawPassword)
    {
        EmailAdress = emailAdress;
        RawPassword = rawPassword;
    }

    public EmailAddress EmailAdress { get; }
    public RawPassword RawPassword { get; set; }
    public static LoginUserQuery MapToQuery(EmailAddress emailAdress, RawPassword rawPassword)
    {
        return new LoginUserQuery(emailAdress, rawPassword);
    }
}