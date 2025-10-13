using DoNet.Domain.Entities;
using Enum = DoNet.Domain.Entities.Enum;

namespace DoNet.Application.Tasks.DTOs;

public sealed record TaskAssignmentDto(
    Guid UserId,
    Guid TaskId,
    Enum.TaskAssignmentRole Role,
    DateTimeOffset AssignedAt)
{
    public static TaskAssignmentDto FromEntity(UserTaskAssignment assignment)
        => new(
            assignment.UserId,
            assignment.TaskItemId,
            assignment.Role,
            assignment.AssignedAt);
}
