using FiapCloudGames.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.Infraestructure.Persistence.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");
        builder.Property(u => u.CreatedAt)
           .IsRequired();
        builder.Property(u => u.CreatedBy)
            .IsRequired();
        builder.Property(u => u.PublicId)
            .IsRequired();
        builder.HasIndex(u => u.PublicId)
            .IsUnique();
        builder.OwnsOne(u => u.FullName, fullName =>
        {
            fullName.Property(fn => fn.Name)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("FullName");
        });
        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(254)
                .HasColumnName("Email");
            email.HasIndex(e => e.Email)
                .IsUnique();
        });
        builder.OwnsOne(u => u.NickName, nickName =>
        {
            nickName.Property(nn => nn.Nick)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("NickName");
            nickName.HasIndex(nn => nn.Nick)
                .IsUnique();
        });
        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(u => u.PasswordSalt)
            .IsRequired()
            .HasMaxLength(128);

    }
}
