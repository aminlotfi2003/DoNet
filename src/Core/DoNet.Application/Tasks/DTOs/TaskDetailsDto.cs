using DoNet.Application.Comments.DTOs;
using DoNet.Domain.Entities;

namespace DoNet.Application.Tasks.DTOs;

public sealed record TaskDetailsDto(
    TaskSummaryDto Task,
    IReadOnlyCollection<TaskAssignmentDto> Assignees,
    IReadOnlyCollection<CommentDto> Comments)
{
    public static TaskDetailsDto FromEntity(TaskItem task)
        => new(
            TaskSummaryDto.FromEntity(task),
            task.Assignees.Select(TaskAssignmentDto.FromEntity).ToList(),
            task.Comments.Select(CommentDto.FromEntity).ToList());
}
