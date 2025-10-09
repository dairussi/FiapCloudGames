using FiapCloudGames.Domain.Users.Entities;
using FiapCloudGames.Domain.Games.Entities;
using Microsoft.EntityFrameworkCore;
using FiapCloudGames.Domain.Promotions.Entities;

namespace FiapCloudGames.Infraestructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Promotion> Promotions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

    }
}