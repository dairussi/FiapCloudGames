using FiapCloudGames.Domain.Users.Entities;
using FiapCloudGames.Domain.Users.Ports;
using FiapCloudGames.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infraestructure.Adapters;

public class UserCommandRepository : IUserCommandRepository
{
    //private readonly AppDbContext _context;

    //public UserCommandRepository(AppDbContext context)
    //{
    //    _context = context;
    //}

    public UserCommandRepository()
    {

    }

    public async Task<User> AddAsync(User user, CancellationToken cancellationToken = default)
    {
        //await _context.Users.AddAsync(user, cancellationToken);
        //await _context.SaveChangesAsync(cancellationToken);
        return user;
    }
}
