using FiapCloudGames.Domain.Games.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.Infraestructure.Persistence.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Game");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Property(g => g.CreatedAt)
            .IsRequired();

        builder.Property(g => g.CreatedBy)
            .IsRequired();

        builder.Property(g => g.PublicId)
          .IsRequired();

        builder.HasIndex(g => g.PublicId)
            .IsUnique();

        builder.Property(g => g.Description)
          .IsRequired()
          .HasMaxLength(500);

        builder.Property(g => g.Genre)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(g => g.ReleaseDate)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(g => g.Developer)
            .IsRequired()
            .HasMaxLength(100);

        builder.OwnsOne(g => g.AgeRating, ageRating =>
        {
            ageRating.Property(ar => ar.Rating)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("AgeRating");

            ageRating.Property(ar => ar.MinimiumAge)
                .IsRequired()
                .HasColumnName("MinimumAge");
        });

        builder.OwnsOne(g => g.Price,
            ar =>
            {
                ar.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("decimal")
                .HasPrecision(18, 2);
            });

        builder.HasMany(g => g.Promotions)
       .WithMany(p => p.Games)
       .UsingEntity(j => j.ToTable("GamePromotion"));

    }
}
