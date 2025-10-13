using DoNet.SharedKernel.Abstractions.Domain;
using static DoNet.Domain.Entities.Enum;

namespace DoNet.Domain.Entities;

public class Project : EntityBase
{
    private readonly List<TaskItem> _tasks = [];
    private readonly List<UserProjectMembership> _members = [];

    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsArchived { get; set; }

    public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();
    public IReadOnlyCollection<UserProjectMembership> Members => _members.AsReadOnly();

    private Project() { } // For EF

    public Project(string name, string? description)
    {
        Name = GuardName(name);
        Description = description?.Trim();
    }

    public static Project Create(string name, string? description = null) => new(name, description);

    public void Rename(string name)
    {
        Name = GuardName(name);
        Touch();
    }

    public void SetDescription(string? description)
    {
        Description = description?.Trim();
        Touch();
    }

    public void Archive()
    {
        if (IsArchived) return;
        IsArchived = true;
        Touch();
    }

    public TaskItem AddTask(string title, string? description = null, TaskPriority priority = TaskPriority.Medium, DateTimeOffset? dueDate = null)
    {
        if (IsArchived)
            throw new InvalidOperationException("Cannot add tasks to an archived project.");
        var task = TaskItem.Create(Id, title, description, priority, dueDate);
        _tasks.Add(task);
        Touch();
        return task;
    }

    public void AddMember(Guid userId, ProjectRole role = ProjectRole.Member, DateTimeOffset? joinedAt = null)
    {
        if (_members.Any(m => m.UserId == userId && m.LeftAt == null))
            return;

        _members.Add(UserProjectMembership.Create(userId, Id, role, joinedAt ?? DateTimeOffset.UtcNow));
        Touch();
    }

    public void RemoveMember(Guid userId, DateTimeOffset? leftAt = null)
    {
        var membership = _members.FirstOrDefault(m => m.UserId == userId && m.LeftAt == null);
        if (membership is null) return;

        membership.MarkLeft(leftAt ?? DateTimeOffset.UtcNow);
        Touch();
    }

    #region Guard Name
    private static string GuardName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Project name is required.", nameof(name));
        var n = name.Trim();
        if (n.Length > 200)
            throw new ArgumentOutOfRangeException(nameof(name), "Project name max length is 200.");
        return n;
    }
    #endregion
}
