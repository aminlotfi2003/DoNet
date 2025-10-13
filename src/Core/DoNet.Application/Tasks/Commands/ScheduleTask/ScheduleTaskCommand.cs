using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Commands.ScheduleTask;

public sealed record ScheduleTaskCommand(Guid TaskId, DateTimeOffset? DueDate) : IRequest<TaskSummaryDto>;
