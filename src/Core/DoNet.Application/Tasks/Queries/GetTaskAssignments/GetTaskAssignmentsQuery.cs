using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Queries.GetTaskAssignments;

public sealed record GetTaskAssignmentsQuery(Guid TaskId) : IRequest<IReadOnlyCollection<TaskAssignmentDto>>;
