using Enum = DoNet.Domain.Entities.Enum;

namespace DoNet.WebFramework.Contracts.Tasks;

public sealed record CreateTaskRequest(
        Guid ProjectId,
        string Title,
        string? Description,
        Enum.TaskPriority Priority,
        DateTimeOffset? DueDate
);
