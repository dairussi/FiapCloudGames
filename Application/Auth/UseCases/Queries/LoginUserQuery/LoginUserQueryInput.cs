using FiapCloudGames.Domain.Users.ValueObjects;

namespace FiapCloudGames.Application.Auth.UseCases.Queries.LoginUserQuery;

public class LoginUserQueryInput
{
    public required string Email { get; set; } = default!;
    public required string Password { get; set; } = default!;
    public LoginUserQuery MapToQuery()
    {
        return new LoginUserQuery(EmailAddress.Create(Email), RawPassword.Create(Password));
    }
}
