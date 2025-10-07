using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Users.UseCases.Commands.AddOrUpdateUser;
using FiapCloudGames.Application.Users.UseCases.Commands.DeactivateUser;
using FiapCloudGames.Application.Users.UseCases.Queries.GetUserById;
using FiapCloudGames.Application.Users.UseCases.Queries.GetUsersPaged;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace FiapCloudGames.API.Controllers.Users;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
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

    [HttpGet]
    public async Task<IActionResult> GetUsersPaged(
        [FromServices] IGetUsersPagedQueryHandler handler,
        CancellationToken cancellationToken,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetUsersPagedQuery(page, pageSize);
        var result = await handler.Handle(query, cancellationToken);

        return result.ToOkActionResult();
    }

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

    [HttpPatch("{publicId}")]
    public async Task<IActionResult> DeactivateUser(
        [FromRoute] Guid publicId,
        [FromServices] IDeactivateUserCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new DeactivateUserCommand(publicId);
        var result = await handler.Handle(command, cancellationToken);
        return result.ToNoContentActionResult();
    }
}
