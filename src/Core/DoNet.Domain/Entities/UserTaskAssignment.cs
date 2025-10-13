using static DoNet.Domain.Entities.Enum;

namespace DoNet.Domain.Entities;

public class UserTaskAssignment
{
    public Guid UserId { get; set; }
    public Guid TaskItemId { get; set; }
    public TaskItem Task { get; set; } = default!;
    public TaskAssignmentRole Role { get; set; }
    public DateTimeOffset AssignedAt { get; set; }

    private UserTaskAssignment() { } // For EF

    private UserTaskAssignment(Guid userId, Guid taskItemId, TaskAssignmentRole role, DateTimeOffset assignedAt)
    {
        UserId = userId != Guid.Empty
            ? userId
            : throw new ArgumentException("UserId is required.", nameof(userId));
        TaskItemId = taskItemId != Guid.Empty
            ? taskItemId
            : throw new ArgumentException("TaskItemId is required.", nameof(taskItemId));
        Role = role;
        AssignedAt = assignedAt;
    }

    public static UserTaskAssignment Create(Guid userId, Guid taskItemId, TaskAssignmentRole role, DateTimeOffset assignedAt)
        => new(userId, taskItemId, role, assignedAt);

    public void ChangeRole(TaskAssignmentRole role) => Role = role;
}
