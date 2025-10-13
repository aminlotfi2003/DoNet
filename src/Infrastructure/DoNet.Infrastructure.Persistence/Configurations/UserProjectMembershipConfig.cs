using DoNet.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DoNet.Infrastructure.Persistence.Configurations;

public class UserProjectMembershipConfig : IEntityTypeConfiguration<UserProjectMembership>
{
    public void Configure(EntityTypeBuilder<UserProjectMembership> builder)
    {
        builder.ToTable("ProjectMembers");

        builder.HasKey(x => new { x.UserId, x.ProjectId, x.JoinedAt });

        // Property
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.ProjectId).IsRequired();
        builder.Property(x => x.Role).IsRequired();
        builder.Property(x => x.JoinedAt).IsRequired();

        // Index
        builder.HasIndex(x => new { x.ProjectId, x.Role });
        builder.HasIndex(x => new { x.UserId, x.ProjectId });

        // Project
        builder.HasOne(x => x.Project)
         .WithMany(p => p.Members)
         .HasForeignKey(x => x.ProjectId)
         .OnDelete(DeleteBehavior.Cascade);

        // without History
        //b.HasKey(x => new { x.UserId, x.ProjectId });
        //b.HasIndex(x => x.ProjectId);
    }
}
