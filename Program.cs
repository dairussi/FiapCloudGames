using FiapCloudGames.Application.Games.UseCases.Commands.AddGame;
using FiapCloudGames.Application.Games.UseCases.Queries.GetGameById;
using FiapCloudGames.Application.Games.UseCases.Queries.GetGamesPaged;
using FiapCloudGames.Application.Promotions.UseCases.Commands.AddPromotion;
using FiapCloudGames.Application.Users.UseCases.Commands.AddOrUpdateUser;
using FiapCloudGames.Application.Users.UseCases.Commands.DeactivateUser;
using FiapCloudGames.Application.Users.UseCases.Queries.GetUserById;
using FiapCloudGames.Application.Users.UseCases.Queries.GetUsersPaged;
using FiapCloudGames.Domain.Common.Ports;
using FiapCloudGames.Domain.Games.Ports;
using FiapCloudGames.Domain.Promotions.Ports;
using FiapCloudGames.Domain.Users.Ports;
using FiapCloudGames.Infraestructure.Adapters.Common;
using FiapCloudGames.Infraestructure.Adapters.Games.Repositories;
using FiapCloudGames.Infraestructure.Adapters.Promotions.Repositories;
using FiapCloudGames.Infraestructure.Adapters.Users.Repositories;
using FiapCloudGames.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddScoped<IHashHelper, HashHelper>();

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


builder.Services.AddScoped<IAddOrUpdatePromotionCommandHandler, AddOrUpdatePromotionCommandHandler>();
builder.Services.AddScoped<IPromotionCommandRepository, PromotionCommandRepository>();
builder.Services.AddScoped<IPromotionQueryRepository, PromotionQueryRepository>();




;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
