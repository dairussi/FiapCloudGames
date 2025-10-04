using FiapCloudGames.Domain.Users.Entities;
using FiapCloudGames.Domain.Games.Entities;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infraestructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>()
            .OwnsOne(g => g.AgeRating);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

    }
}