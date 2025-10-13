using DoNet.Application.Tasks.DTOs;
using MediatR;
using Enum = DoNet.Domain.Entities.Enum;

namespace DoNet.Application.Tasks.Commands.ChangeTaskPriority;

public sealed record ChangeTaskPriorityCommand(Guid TaskId, Enum.TaskPriority Priority) : IRequest<TaskSummaryDto>;
