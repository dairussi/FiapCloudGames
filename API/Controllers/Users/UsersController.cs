using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Users.UseCases.Commands.AddOrUpdateUser;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddOrUpdateUser(
        [FromBody] AddOrUpdateUserInput input,
        [FromServices] IAddOrUpdateUserCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var command = input.MapToCommand();
        var result = await handler.Handle(command, cancellationToken);
        return result.ToCreatedActionResult("/pathdogetbyidaqui");
    }
}
