using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Commands.UpdateTaskDescription;

public sealed record UpdateTaskDescriptionCommand(Guid TaskId, string? Description) : IRequest<TaskSummaryDto>;
