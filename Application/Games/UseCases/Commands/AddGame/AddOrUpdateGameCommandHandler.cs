using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Games.Entities;
using FiapCloudGames.Domain.Games.Ports;


namespace FiapCloudGames.Application.Games.UseCases.Commands.AddGame;

public class AddOrUpdateGameCommandHandler : IAddOrUpdateGameCommandHandler
{
    private readonly IGameQueryRepository _gameQueryRepository;
    private readonly IGameCommandRepository _gameCommandRepository;

    public AddOrUpdateGameCommandHandler(IGameQueryRepository gameQueryRepository, IGameCommandRepository gameCommandRepository)
    {
        _gameQueryRepository = gameQueryRepository;
        _gameCommandRepository = gameCommandRepository;
    }

    public async Task<ResultData<Game>> Handle(AddOrUpdateGameCommand command, CancellationToken cancellationToken)
    {
        var gameExists = await _gameQueryRepository.GameExistsAsync(
                command.PublicId,
                command.Description,
                command.Developer,
                cancellationToken
            );

        if (gameExists)
            return ResultData<Game>.Error("Já existe um jogo com a mesma descrição e desenvolvedora.");

        Game game;

        if (command.PublicId.HasValue)
        {
            game = await _gameQueryRepository.GetByIdAsync(command.PublicId.Value, cancellationToken);

            if (game == null)
                return ResultData<Game>.Error("Registro não encontrado.");

            game.UpdateDetails(
                command.Description,
                command.Genre,
                command.ReleaseDate,
                command.Developer,
                command.Price,
                command.AgeRating
            );
        }
        else
        {
            game = Game.Create(
                command.Description,
                command.Genre,
                command.ReleaseDate,
                command.Developer,
                command.Price,
                command.AgeRating,
                command.CreatedBy
            );
        }
        await _gameCommandRepository.SaveAsync(game, cancellationToken);
        return ResultData<Game>.Success(game);
    }
}
