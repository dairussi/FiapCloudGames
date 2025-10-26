using FiapCloudGames.Application.GamePurchases.UseCases.Commands.AddGamePurchase;
using FiapCloudGames.Application.GamePurchases.UseCases.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Controllers.GamePurchases;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GamePurchaseController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetByUserGamePurchases(
        [FromServices] IGetByUserGamePurchasesQueryHandler handle,
        CancellationToken cancellationToken,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetByUserGamePurchaseQuery(page, pageSize);
        var result = await handle.Handle(query, cancellationToken);

        return result.ToOkActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> AddGamePurchases(
        [FromBody] AddGamePurchasesComand input,
        [FromServices] IAddGamePurchasesCommandHandler handle,
        CancellationToken cancellationToken)
    {
        var result = await handle.Handle(input, cancellationToken);
        return result.ToCreatedActionResult($"/api/gamepurchases");
    }

}