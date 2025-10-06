using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Users.UseCases.Commands.AddOrUpdateUser;
using FiapCloudGames.Application.Users.UseCases.Queries.GetUserById;
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
        return result.ToCreatedActionResult($"/api/users/{result.Data.PublicId}");
    }

    [HttpGet("{publicId}")]
    public async Task<IActionResult> GetUserById(
        [FromRoute] Guid publicId,
        [FromServices] IGetUserByIdQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(publicId);
        var result = await handler.Handle(query, cancellationToken);
        return result.ToOkActionResult();
    }
}
