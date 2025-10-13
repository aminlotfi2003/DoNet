using DoNet.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DoNet.Infrastructure.Persistence.Configurations;

public class UserTaskAssignmentConfig : IEntityTypeConfiguration<UserTaskAssignment>
{
    public void Configure(EntityTypeBuilder<UserTaskAssignment> builder)
    {
        builder.ToTable("TaskAssignees");

        builder.HasKey(x => new { x.UserId, x.TaskItemId });

        // Property
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.TaskItemId).IsRequired();
        builder.Property(x => x.Role).IsRequired();
        builder.Property(x => x.AssignedAt).IsRequired();

        // Index
        builder.HasIndex(x => new { x.TaskItemId, x.Role });

        // Relation
        builder.HasOne(x => x.Task)
         .WithMany(t => t.Assignees)
         .HasForeignKey(x => x.TaskItemId)
         .OnDelete(DeleteBehavior.Cascade);
    }
}
