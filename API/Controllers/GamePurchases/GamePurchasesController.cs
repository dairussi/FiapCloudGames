using FiapCloudGames.Application.GamePurchases.UseCases.Commands.AddGamePurchase;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Controllers.GamePurchases;

[ApiController]
[Route("api/[controller]")]
public class GamePurchaseController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> GamePurchases(
        [FromBody] AddGamePurchasesComand input,
        [FromServices] IAddGamePurchasesCommandHandler handle,
        CancellationToken cancellationToken)
    {
        var result = await handle.Handle(input, cancellationToken);
        return result.ToCreatedActionResult($"/api/gamepurchases");
    }
}
