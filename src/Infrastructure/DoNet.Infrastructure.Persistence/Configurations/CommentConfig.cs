using DoNet.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DoNet.Infrastructure.Persistence.Configurations;

public class CommentConfig : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(x => x.Id);

        // Property
        builder.Property(x => x.TaskItemId).IsRequired();
        builder.Property(x => x.AuthorId).IsRequired();
        builder.Property(x => x.Body).IsRequired().HasMaxLength(4000);

        // Index
        builder.HasIndex(x => x.TaskItemId);
        builder.HasIndex(x => x.AuthorId);
    }
}
