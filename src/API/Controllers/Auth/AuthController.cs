using FiapCloudGames.Application.Auth.UseCases.Queries.LoginUserQuery;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILoginUserQueryHandler _loginUserQueryHandler;
    public AuthController(ILoginUserQueryHandler loginUserQueryHandler)
    {
        _loginUserQueryHandler = loginUserQueryHandler;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserQueryInput input, CancellationToken cancellationToken)
    {
        var query = input.MapToQuery();
        var result = await _loginUserQueryHandler.Handle(query, cancellationToken);

        if (result == null)
            return new UnauthorizedObjectResult(new { message = "Usuário ou senha inválidos" });

        return result.ToOkActionResult();
    }
}