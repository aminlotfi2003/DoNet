using static DoNet.Domain.Entities.Enum;

namespace DoNet.Domain.Entities;

public class UserProjectMembership
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = default!;
    public ProjectRole Role { get; set; } = ProjectRole.Member;
    public DateTimeOffset JoinedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? LeftAt { get; set; }

    private UserProjectMembership() { } // For EF

    private UserProjectMembership(Guid userId, Guid projectId, ProjectRole role, DateTimeOffset joinedAt)
    {
        UserId = userId != Guid.Empty
            ? userId
            : throw new ArgumentException("UserId is required.", nameof(userId));
        ProjectId = projectId != Guid.Empty
            ? projectId
            : throw new ArgumentException("ProjectId is required.", nameof(projectId));
        Role = role;
        JoinedAt = joinedAt;
    }

    public static UserProjectMembership Create(Guid userId, Guid projectId, ProjectRole role, DateTimeOffset joinedAt)
        => new(userId, projectId, role, joinedAt);

    public void ChangeRole(ProjectRole role) => Role = role;

    public void MarkLeft(DateTimeOffset leftAt)
    {
        if (LeftAt is not null) return;
        LeftAt = leftAt;
    }
}
