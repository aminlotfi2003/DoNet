using DoNet.SharedKernel.Abstractions.Domain;
using static DoNet.Domain.Entities.Enum;
using TaskStatus = DoNet.Domain.Entities.Enum.TaskStatus;

namespace DoNet.Domain.Entities;

public class TaskItem : EntityBase
{
    private readonly List<Comment> _comments = [];
    private readonly List<UserTaskAssignment> _assignees = [];

    public Guid ProjectId { get; set; }

    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.Todo;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateTimeOffset? DueDate { get; set; }
    public bool IsArchived { get; set; }

    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
    public IReadOnlyCollection<UserTaskAssignment> Assignees => _assignees.AsReadOnly();

    private TaskItem() { } // For EF

    public TaskItem(Guid projectId, string title, string? description, TaskPriority priority, DateTimeOffset? dueDate)
    {
        ProjectId = projectId != Guid.Empty
            ? projectId
            : throw new ArgumentException("ProjectId is required.", nameof(projectId));
        Title = GuardTitle(title);
        Description = description?.Trim();
        Priority = priority;
        DueDate = dueDate;
    }

    public static TaskItem Create(Guid projectId, string title, string? description = null, TaskPriority priority = TaskPriority.Medium, DateTimeOffset? dueDate = null)
        => new(projectId, title, description, priority, dueDate);

    public void Rename(string title)
    {
        Title = GuardTitle(title);
        Touch();
    }

    public void ChangeDescription(string? description)
    {
        Description = description?.Trim();
        Touch();
    }

    public void ChangePriority(TaskPriority priority)
    {
        Priority = priority;
        Touch();
    }

    public void ChangeStatus(TaskStatus status)
    {
        if (IsArchived && status != TaskStatus.Archived)
            throw new InvalidOperationException("Archived task cannot change status.");
        Status = status;
        Touch();
    }

    public void Schedule(DateTimeOffset? dueDate)
    {
        DueDate = dueDate;
        Touch();
    }

    public void Archive()
    {
        if (IsArchived) return;
        IsArchived = true;
        Status = TaskStatus.Archived;
        Touch();
    }

    public Comment AddComment(Guid authorId, string body)
    {
        if (string.IsNullOrWhiteSpace(body))
            throw new ArgumentException("Comment body is required.", nameof(body));
        var c = Comment.Create(Id, authorId, body.Trim());
        _comments.Add(c);
        Touch();
        return c;
    }

    public void AssignUser(Guid userId, TaskAssignmentRole role = TaskAssignmentRole.Contributor, DateTimeOffset? at = null)
    {
        if (_assignees.Any(a => a.UserId == userId))
            return;
        _assignees.Add(UserTaskAssignment.Create(userId, Id, role, at ?? DateTimeOffset.UtcNow));
        Touch();
    }

    public void UnassignUser(Guid userId)
    {
        var a = _assignees.FirstOrDefault(x => x.UserId == userId);
        if (a is null) return;
        _assignees.Remove(a);
        Touch();
    }

    #region Guard Title
    private static string GuardTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Task title is required.", nameof(title));
        var t = title.Trim();
        if (t.Length > 200) throw new ArgumentOutOfRangeException(nameof(title), "Task title max length is 200.");
        return t;
    }
    #endregion
}
