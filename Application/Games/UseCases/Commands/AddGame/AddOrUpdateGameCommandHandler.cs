using FiapCloudGames.Application.Common;
using FiapCloudGames.Domain.Games.Entities;


namespace FiapCloudGames.Application.Games.UseCases.Commands.AddGame;

public class AddOrUpdateGameCommandHandler : IAddOrUpdateGameCommandHandler
{
    public AddOrUpdateGameCommandHandler()
    {
        
    }

    public Task<ResultData<Game>> Handle(AddOrUpdateGameCommand command, CancellationToken cancellationToken)
    {
        //incluir aqui a validação GameExistsAsync;
        var game = Game.Create(command.Description,command.Genre,command.ReleaseDate,command.Developer,command.Price,command.AgeRating);
        //persistencia DB

        return Task.FromResult(ResultData<Game>.Success(game));
    }
}
