using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Commands.RenameTask;

public sealed record RenameTaskCommand(Guid TaskId, string Title) : IRequest<TaskSummaryDto>;
