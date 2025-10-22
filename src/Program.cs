using FiapCloudGames.Application.Auth.UseCases.Queries.LoginUserQuery;
using FiapCloudGames.Application.GamePurchases.UseCases.Commands.AddGamePurchase;
using FiapCloudGames.Application.GamePurchases.UseCases.Queries;
using FiapCloudGames.Application.Games.UseCases.Commands.AddGame;
using FiapCloudGames.Application.Games.UseCases.Queries.GetGameById;
using FiapCloudGames.Application.Games.UseCases.Queries.GetGamesPaged;
using FiapCloudGames.Application.Promotions.UseCases.Commands.AddPromotion;
using FiapCloudGames.Application.Promotions.UseCases.Queries.GetPromotionById;
using FiapCloudGames.Application.Promotions.UseCases.Queries.GetPromotionsPaged;
using FiapCloudGames.Application.Users.UseCases.Commands.AddOrUpdateUser;
using FiapCloudGames.Application.Users.UseCases.Commands.DeactivateUser;
using FiapCloudGames.Application.Users.UseCases.Queries.GetUserById;
using FiapCloudGames.Application.Users.UseCases.Queries.GetUsersPaged;
using FiapCloudGames.Domain.Common.Ports;
using FiapCloudGames.Domain.GamePurchases.Ports;
using FiapCloudGames.Domain.Games.Ports;
using FiapCloudGames.Domain.Promotions.Ports;
using FiapCloudGames.Domain.Users.Ports;
using FiapCloudGames.Infraestructure.Adapters.Common;
using FiapCloudGames.Infraestructure.Adapters.GamePurchases;
using FiapCloudGames.Infraestructure.Adapters.Games.Repositories;
using FiapCloudGames.Infraestructure.Adapters.Inbound.Middleware;
using FiapCloudGames.Infraestructure.Adapters.Promotions.Repositories;
using FiapCloudGames.Infraestructure.Adapters.Promotions.Services;
using FiapCloudGames.Infraestructure.Adapters.Users.Repositories;
using FiapCloudGames.Infraestructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Scoped);

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IHashHelper, HashHelper>();
builder.Services.AddScoped<IUserContext, UserContext>();

builder.Services.AddScoped<ILoginUserQueryHandler, LoginUserQueryHandler>();

builder.Services.AddScoped<IAddOrUpdatePromotionCommandHandler, AddOrUpdatePromotionCommandHandler>();
builder.Services.AddScoped<IPromotionCommandRepository, PromotionCommandRepository>();
builder.Services.AddScoped<IPromotionQueryRepository, PromotionQueryRepository>();
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<IGetPromotionByIdQueryHandler, GetPromotionByIdQueryHandler>();
builder.Services.AddScoped<IGetPromotionsPagedQueryHandler, GetPromotionsPagedQueryHandler>();

builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();
builder.Services.AddScoped<IGetUserByIdQueryHandler, GetUserByIdQueryHandler>();
builder.Services.AddScoped<IGetUsersPagedQueryHandler, GetUsersPagedQueryHandler>();
builder.Services.AddScoped<IDeactivateUserCommandHandler, DeactivateUserCommandHandler>();
builder.Services.AddScoped<IAddOrUpdateUserCommandHandler, AddOrUpdateUserCommandHandler>();

builder.Services.AddScoped<IAddOrUpdateGameCommandHandler, AddOrUpdateGameCommandHandler>();
builder.Services.AddScoped<IGameCommandRepository, GameCommandRepository>();
builder.Services.AddScoped<IGameQueryRepository, GameQueryRepository>();
builder.Services.AddScoped<IGetGameByIdQueryHandler, GetGameByIdQueryHandler>();
builder.Services.AddScoped<IGetGamesPagedQueryHandler, GetGamesPagedQueryHandler>();

builder.Services.AddScoped<IAddGamePurchasesCommandHandler, AddGamePurchasesCommandHandler>();
builder.Services.AddScoped<IGamePurchaseCommandRepository, GamePurchasesCommandRepository>();
builder.Services.AddScoped<IGamePurchaseQueryRepository, GamePurchaseQueryRepository>();
builder.Services.AddScoped<IGetByUserGamePurchasesQueryHandler, GetByUserGamePurchasesQueryHandler>();




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtSecretKey = configuration["Jwt:SecretKey"]
    ?? throw new InvalidOperationException("JWT SecretKey não configurada no appsettings.json");

var key = Encoding.ASCII.GetBytes(jwtSecretKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false; // Em produção true
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
