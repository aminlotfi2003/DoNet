namespace DoNet.Domain.Entities;

public class Enum
{
    public enum ProjectRole { Owner = 1, Maintainer = 2, Member = 3 }

    public enum TaskAssignmentRole { Owner = 1, Contributor = 2, Reviewer = 3 }

    public enum TaskPriority { Low = 0, Medium = 1, High = 2, Critical = 3 }

    public enum TaskStatus { Todo = 0, InProgress = 1, Done = 2, Blocked = 3, Archived = 9 }
}
