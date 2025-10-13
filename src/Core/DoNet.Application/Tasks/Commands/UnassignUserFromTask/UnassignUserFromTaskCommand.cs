using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Commands.UnassignUserFromTask;

public sealed record UnassignUserFromTaskCommand(Guid TaskId, Guid UserId) : IRequest<TaskAssignmentDto?>;
