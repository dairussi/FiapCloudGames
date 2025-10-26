using FiapCloudGames.Application.Common;
using FiapCloudGames.Application.Games.Mappers;
using FiapCloudGames.Application.Games.Outputs;
using FiapCloudGames.Domain.Games.Entities;
using FiapCloudGames.Domain.Games.Ports;


namespace FiapCloudGames.Application.Games.UseCases.Commands.AddGame;

public class AddOrUpdateGameCommandHandler : IAddOrUpdateGameCommandHandler
{
    private readonly IGameCommandRepository _gameCommandRepository;

    public AddOrUpdateGameCommandHandler(IGameCommandRepository gameCommandRepository)
    {
        _gameCommandRepository = gameCommandRepository;
    }

    public async Task<ResultData<GameOutput>> Handle(AddOrUpdateGameCommand command, CancellationToken cancellationToken)
    {
        var gameExists = await _gameCommandRepository.GameExistsAsync(
                command.PublicId,
                command.Description,
                command.Developer,
                cancellationToken
            );

        if (gameExists)
            return ResultData<GameOutput>.Error("Já existe um jogo com a mesma descrição e desenvolvedora.");

        Game game;

        if (command.PublicId.HasValue)
        {
            game = await _gameCommandRepository.GetByIdAsync(command.PublicId.Value, cancellationToken);

            if (game == null)
                return ResultData<GameOutput>.Error("Registro não encontrado.");

            game.UpdateDetails(
                command.Name,
                command.Description,
                command.Genre,
                command.ReleaseDate,
                command.Developer,
                command.Price,
                command.AgeRating
            );
            await _gameCommandRepository.UpdateAsync(game, cancellationToken);
        }
        else
        {
            game = Game.Create(
                command.Name,
                command.Description,
                command.Genre,
                command.ReleaseDate,
                command.Developer,
                command.Price,
                command.AgeRating
            );

            await _gameCommandRepository.AddAsync(game, cancellationToken);
        }

        var gameOutput = game.ToOutput();

        return ResultData<GameOutput>.Success(gameOutput);
    }
}