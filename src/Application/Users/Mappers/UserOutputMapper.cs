using FiapCloudGames.Application.Users.Outputs;
using FiapCloudGames.Domain.Users.Entities;

namespace FiapCloudGames.Application.Users.Mappers;

public static class UserOutputMapper
{
    public static UserOutput ToOutput(this User user)
    {
        return new UserOutput(
            user.PublicId,
            user.FullName.Name,
            user.Email.Email,
            user.NickName.Nick,
            user.IsActive,
            user.Role.ToString()
        );
    }

    public static List<UserOutput> ToOutput(this IEnumerable<User> users)
    {
        return users.Select(ToOutput).ToList();
    }
}