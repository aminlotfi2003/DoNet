using DoNet.Application.Tasks.DTOs;
using MediatR;
using Enum = DoNet.Domain.Entities.Enum;

namespace DoNet.Application.Tasks.Commands.ChangeTaskStatus;

public sealed record ChangeTaskStatusCommand(Guid TaskId, Enum.TaskStatus Status) : IRequest<TaskSummaryDto>;
