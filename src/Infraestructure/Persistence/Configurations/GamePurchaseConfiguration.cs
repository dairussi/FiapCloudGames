using FiapCloudGames.Domain.GamePurchases.Entities;
using FiapCloudGames.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.Infraestructure.Persistence.Configurations;

public class GamePurchaseConfigurations : IEntityTypeConfiguration<GamePurchase>
{
    public void Configure(EntityTypeBuilder<GamePurchase> builder)
    {
        builder.ToTable("GamePurchase");

        builder.HasKey(gp => gp.Id);

        builder.Property(gp => gp.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Property(gp => gp.CreatedAt)
            .IsRequired();

        builder.Property(gp => gp.CreatedBy)
            .IsRequired();

        builder.Property(gp => gp.UserId)
            .IsRequired();

        builder.Property(gp => gp.GameId)
            .IsRequired();

        builder.Property(gp => gp.PromotionId)
            .IsRequired(false);

        builder.Property(gp => gp.DataGamePurchase)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.OwnsOne(gp => gp.FinalPrice, price =>
        {
            price.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("FinalPrice")
                .HasColumnType("decimal")
                .HasPrecision(18, 2);
        });

        builder.OwnsOne(gp => gp.PromotionValue, promotionValue =>
        {
            promotionValue.Property(pv => pv.Value)
                .HasColumnName("PromotionValue")
                .HasColumnType("decimal")
                .HasPrecision(18, 2);
        });

        builder.HasOne(gp => gp.Game)
            .WithMany()
            .HasForeignKey(gp => gp.GameId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<User>()
            .WithMany(u => u.GamePurchases)
            .HasForeignKey(gp => gp.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}