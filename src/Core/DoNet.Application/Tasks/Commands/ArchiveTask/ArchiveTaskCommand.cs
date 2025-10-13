using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Commands.ArchiveTask;

public sealed record ArchiveTaskCommand(Guid TaskId) : IRequest<TaskSummaryDto>;
