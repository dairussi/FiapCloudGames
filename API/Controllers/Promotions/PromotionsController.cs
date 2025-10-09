using FiapCloudGames.Application.Promotions.UseCases.Commands.AddPromotion;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Controllers.Promotions;

[ApiController]
[Route("api/[controller]")]
public class PromotionsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddOrUpdatePromotion(
        [FromBody] AddOrUpdatePromotionInput input,
        [FromServices] IAddOrUpdatePromotionCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var command = input.MapToCommand();
        var result = await handler.Handle(command, cancellationToken);
        return result.ToCreatedActionResult($"/api/promotions/{result.Data.PublicId}");
    }
}
