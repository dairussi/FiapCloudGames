using FiapCloudGames.Application.Games.UseCases.Commands.AddGame;
using FiapCloudGames.Application.Users.UseCases.Commands.AddOrUpdateUser;
using FiapCloudGames.Domain.Common.Ports;
using FiapCloudGames.Domain.Games.Ports;
using FiapCloudGames.Infraestructure.Adapters.Common;
using FiapCloudGames.Infraestructure.Adapters.Games.Repositories;
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

builder.Services.AddScoped<IAddOrUpdateUserCommandHandler, AddOrUpdateUserCommandHandler>();
builder.Services.AddScoped<IHashHelper, HashHelper>();
builder.Services.AddScoped<IAddOrUpdateGameCommandHandler, AddOrUpdateGameCommandHandler>();
builder.Services.AddScoped<IGameCommandRepository, GameCommandRepository>();
builder.Services.AddScoped<IGameQueryRepository, GameQueryRepository>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Scoped);

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
