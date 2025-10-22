using FiapCloudGames.Domain.Common.Ports;
using System.Security.Claims;

namespace FiapCloudGames.Infraestructure.Adapters.Common;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User
          .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            throw new UnauthorizedAccessException("Usuário não autenticado");

        return int.Parse(userIdClaim);
    }

}
