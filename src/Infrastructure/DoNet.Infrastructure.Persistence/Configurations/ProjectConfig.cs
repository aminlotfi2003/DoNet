using DoNet.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DoNet.Infrastructure.Persistence.Configurations;

public class ProjectConfig : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");

        // Property
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Description).HasMaxLength(2000);
        builder.Property(x => x.IsArchived).IsRequired();

        // Index
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.IsArchived);

        // Relation
        builder.HasMany(x => x.Tasks)
         .WithOne()
         .HasForeignKey(t => t.ProjectId)
         .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Members)
         .WithOne()
         .HasForeignKey(m => m.ProjectId)
         .OnDelete(DeleteBehavior.Cascade);
    }
}
