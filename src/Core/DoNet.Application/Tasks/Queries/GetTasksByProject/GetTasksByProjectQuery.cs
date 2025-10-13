using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Queries.GetTasksByProject;

public sealed record GetTasksByProjectQuery(Guid ProjectId, bool IncludeArchived = false) : IRequest<IReadOnlyCollection<TaskSummaryDto>>;
