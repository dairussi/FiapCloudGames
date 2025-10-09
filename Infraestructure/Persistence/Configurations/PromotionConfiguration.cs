using FiapCloudGames.Domain.Promotions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.Infraestructure.Persistence.Configurations;

public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
{
    public void Configure(EntityTypeBuilder<Promotion> builder)
    {
        builder.ToTable("Promotion");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");
        builder.Property(p => p.CreatedAt)
            .IsRequired();
        builder.Property(p => p.CreatedBy)
            .IsRequired();
        builder.Property(p => p.PublicId)
          .IsRequired();
        builder.HasIndex(p => p.PublicId)
            .IsUnique();
        builder.Property(p => p.Description)
          .IsRequired()
          .HasMaxLength(500);
        builder.OwnsOne(p => p.Period, period =>
        {
            period.Property(pe => pe.StartDate).IsRequired().HasColumnName("StartDate");
            period.Property(pe => pe.EndDate).IsRequired().HasColumnName("EndDate");
        });
        builder.OwnsOne(p => p.DiscountRule, discountRule =>
        {
            discountRule.Property(dr => dr.Type).IsRequired().HasConversion<string>().HasMaxLength(50).HasColumnName("DiscountType");
            discountRule.Property(dr => dr.Percentage).HasColumnName("Percentage").HasPrecision(5, 2);
            discountRule.Property(dr => dr.FixedAmount).HasColumnName("FixedAmount").HasPrecision(18, 2);
        });
        builder.Property(p => p.Status).IsRequired().HasConversion<string>().HasMaxLength(50);
    }
}
