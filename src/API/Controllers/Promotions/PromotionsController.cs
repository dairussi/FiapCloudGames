using FiapCloudGames.Application.Promotions.UseCases.Commands.AddPromotion;
using FiapCloudGames.Application.Promotions.UseCases.Queries.GetPromotionById;
using FiapCloudGames.Application.Promotions.UseCases.Queries.GetPromotionsPaged;
using FiapCloudGames.Domain.Common.Enuns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Controllers.Promotions;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PromotionsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPromotionsPaged(
        [FromServices] IGetPromotionsPagedQueryHandler handler,
        CancellationToken cancellationToken,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetPromotionsPagedQuery(page, pageSize);
        var result = await handler.Handle(query, cancellationToken);
        return result.ToOkActionResult();
    }

    [HttpGet("{publicId}")]
    public async Task<IActionResult> GetPromotionById(
        [FromRoute] Guid publicId,
        [FromServices] IGetPromotionByIdQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var query = new GetPromotionByIdQuery(publicId);
        var result = await handler.Handle(query, cancellationToken);
        return result.ToOkActionResult();
    }

    [Authorize(Roles = nameof(EUserRole.Admin))]
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