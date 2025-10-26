using FiapCloudGames.Application.Games.UseCases.Commands.AddGame;
using FiapCloudGames.Application.Games.UseCases.Queries.GetGameById;
using FiapCloudGames.Application.Games.UseCases.Queries.GetGamesPaged;
using FiapCloudGames.Domain.Common.Enuns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Controllers.Games;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GamesController : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetGamesPaged(
        [FromServices] IGetGamesPagedQueryHandler handler,
        CancellationToken cancellationToken,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetGamesPagedQuery(page, pageSize);
        var result = await handler.Handle(query, cancellationToken);

        return result.ToOkActionResult();
    }

    [HttpGet("{publicId}")]
    public async Task<IActionResult> GetGameById(
        [FromRoute] Guid publicId,
        [FromServices] IGetGameByIdQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var query = new GetGameByIdQuery(publicId);
        var result = await handler.Handle(query, cancellationToken);
        return result.ToOkActionResult();
    }

    [Authorize(Roles = nameof(EUserRole.Admin))]
    [HttpPost]
    public async Task<IActionResult> AddOrUpdatGame(
        [FromBody] AddOrUpdateGameInput input,
        [FromServices] IAddOrUpdateGameCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var command = input.MapToCommand();
        var result = await handler.Handle(command, cancellationToken);
        return result.ToCreatedActionResult($"/api/games/{result.Data.PublicId}");
    }
}