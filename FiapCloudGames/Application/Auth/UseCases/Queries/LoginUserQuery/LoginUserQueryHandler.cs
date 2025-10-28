using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Common.Enuns;
using FiapCloudGames.Domain.Common.Ports;
using FiapCloudGames.Domain.Users.Ports;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FiapCloudGames.Application.Auth.UseCases.Queries.LoginUserQuery;

public class LoginUserQueryHandler : ILoginUserQueryHandler
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IHashHelper _hashHelper;
    private readonly IConfiguration _configuration;

    public LoginUserQueryHandler(
        IUserQueryRepository userQueryRepository,
        IHashHelper hashHelper,
        IConfiguration configuration)
    {
        _userQueryRepository = userQueryRepository;
        _hashHelper = hashHelper;
        _configuration = configuration;
    }
    public async Task<ResultData<LoginUserQueryOutput>> Handle(LoginUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _userQueryRepository.GetByEmailAsync(query.EmailAdress, cancellationToken);

        if (user == null)
            return ResultData<LoginUserQueryOutput>.Error("Usuário ou senha inválidos");

        if (!user.IsActive)
            return ResultData<LoginUserQueryOutput>.Error("Usuário inativo");

        if (!_hashHelper.VerifyHash(query.RawPassword, user.PasswordHash, user.PasswordSalt))
            return ResultData<LoginUserQueryOutput>.Error("Usuário ou senha inválidos");

        var token = GerarToken(user.Id, user.Role);

        var output = new LoginUserQueryOutput
        {
            Token = token,
            UserId = user.Id,
            Role = user.Role.ToString()
        };

        return ResultData<LoginUserQueryOutput>.Success(output);
    }

    private string GerarToken(int userId, EUserRole role)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]!);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, role.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}