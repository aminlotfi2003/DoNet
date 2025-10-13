using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Queries.GetTaskById;

public sealed record GetTaskByIdQuery(Guid TaskId) : IRequest<TaskSummaryDto?>;

