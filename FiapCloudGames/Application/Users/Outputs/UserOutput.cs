namespace FiapCloudGames.Application.Users.Outputs;

public record UserOutput(
    Guid PublicId,
    string FullName,
    string Email,
    string NickName,
    bool IsActive,
    string Role
);