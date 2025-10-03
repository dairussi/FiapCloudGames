using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Users.UseCases.Commands.AddOrUpdateUser;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Controllers.Users;

public class UsersController
{
    [HttpPost]
    public async Task<IActionResult> AddOrUpdateUser(
        [FromBody] AddOrUpdateUserInput input,
        [FromServices] IAddOrUpdateUserCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var command = input.MapToCommand();
        var result = await handler.Handle(command, cancellationToken);
        return result.ToCreatedActionResult("/");
    }
}
