using DoNet.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DoNet.Infrastructure.Persistence.Configurations;

public class TaskItemConfig : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("TaskItems");

        builder.HasKey(x => x.Id);

        // Property
        builder.Property(x => x.ProjectId).IsRequired();
        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Description).HasMaxLength(4000);
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.Priority).IsRequired();
        builder.Property(x => x.IsArchived).IsRequired();

        // Index
        builder.HasIndex(x => new { x.ProjectId, x.Status });
        builder.HasIndex(x => new { x.ProjectId, x.Priority });
        builder.HasIndex(x => x.DueDate);

        // Comments
        builder.HasMany(x => x.Comments)
         .WithOne()
         .HasForeignKey(c => c.TaskItemId)
         .OnDelete(DeleteBehavior.Cascade);

        // Assignees
        builder.HasMany(x => x.Assignees)
         .WithOne()
         .HasForeignKey(a => a.TaskItemId)
         .OnDelete(DeleteBehavior.Cascade);
    }
}
