using DoNet.Application.Tasks.DTOs;
using MediatR;
using Enum = DoNet.Domain.Entities.Enum;

namespace DoNet.Application.Tasks.Commands.AssignUserToTask;

public sealed record AssignUserToTaskCommand(Guid TaskId, Guid UserId, Enum.TaskAssignmentRole Role) : IRequest<TaskAssignmentDto>;
