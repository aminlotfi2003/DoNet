using DoNet.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoNet.Infrastructure.Identity.Configurations;

public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");

        // Property
        builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Gender).IsRequired();
        builder.Property(x => x.BirthDate).IsRequired();
        builder.Property(x => x.IsActived).IsRequired();

        // Index
        builder.HasIndex(x => x.FirstName);
        builder.HasIndex(x => x.LastName);
        builder.HasIndex(x => x.BirthDate);
        builder.HasIndex(X => X.IsActived);

        // Relation
        builder.HasMany(x => x.RefreshTokens)
            .WithOne(t => t.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
