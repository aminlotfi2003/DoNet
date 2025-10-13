using DoNet.Application.Tasks.DTOs;
using MediatR;
using Enum = DoNet.Domain.Entities.Enum;

namespace DoNet.Application.Tasks.Commands.CreateTask;

public sealed record CreateTaskCommand(
    Guid ProjectId,
    string Title,
    string? Description,
    Enum.TaskPriority Priority,
    DateTimeOffset? DueDate) : IRequest<TaskSummaryDto>;
