
using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Games.UseCases.Commands.AddGame;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Controllers.Games;

[ApiController]
[Route("api/games")]
public class GamesController : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> AddOrUpdatGame(
        [FromBody] AddOrUpdateGameInput input,
        [FromServices] IAddOrUpdateGameCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var command = input.MapToCommand();
        var result = await handler.Handle(command, cancellationToken);
        return result.ToCreatedActionResult("/");
    }
}
