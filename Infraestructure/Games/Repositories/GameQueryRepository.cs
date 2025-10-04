using FiapCloudGames.Domain.Games.Ports;

namespace FiapCloudGames.Infraestructure.Games.Repositories;

public class GameQueryRepository : IGameQueryRepository
{
    //private readonly dbcontext;

    public async Task<bool> GameExistsAsync(string description, string developer, CancellationToken cancellationToken)
    {
        //return await _context.Games.AnyAsync(
        //    g => g.Description == description && g.Developer == developer,
        //    cancellationToken
        //);
        return true;
    }
}
