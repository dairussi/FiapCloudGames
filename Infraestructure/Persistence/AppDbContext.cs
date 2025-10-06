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

        modelBuilder.Entity<Game>(game =>
        {
            game.OwnsOne(g => g.AgeRating,
                ar =>
                {
                    ar.Property(a => a.Rating).HasColumnName("AgeRating");
                    ar.Property(a => a.MinimiumAge).HasColumnName("MinimiumAge");
                });

            game.OwnsOne(g => g.Price,
                ar =>
                {
                    ar.Property(p => p.Value).HasColumnName("Price").HasPrecision(18, 2);
                });


        });

        modelBuilder.Entity<User>(user =>
        {
            user.OwnsOne(u => u.Email,
                e =>
                {
                    e.Property(em => em.Email).HasColumnName("Email");
                });

            user.OwnsOne(u => u.FullName, fn =>
            {
                fn.Property(f => f.Name).HasColumnName("FullName");
            });

            user.OwnsOne(u => u.NickName, fn =>
            {
                fn.Property(f => f.Nick).HasColumnName("NickName");
            });

        });

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

    }
}