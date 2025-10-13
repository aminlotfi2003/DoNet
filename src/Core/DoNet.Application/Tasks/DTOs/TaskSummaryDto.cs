using DoNet.Domain.Entities;
using Enum = DoNet.Domain.Entities.Enum;

namespace DoNet.Application.Tasks.DTOs;

public sealed record TaskSummaryDto(
    Guid Id,
    Guid ProjectId,
    string Title,
    string? Description,
    Enum.TaskStatus Status,
    Enum.TaskPriority Priority,
    DateTimeOffset? DueDate,
    bool IsArchived,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt)
{
    public static TaskSummaryDto FromEntity(TaskItem task)
        => new(
            task.Id,
            task.ProjectId,
            task.Title,
            task.Description,
            task.Status,
            task.Priority,
            task.DueDate,
            task.IsArchived,
            task.CreatedAt,
            task.UpdatedAt);
}
