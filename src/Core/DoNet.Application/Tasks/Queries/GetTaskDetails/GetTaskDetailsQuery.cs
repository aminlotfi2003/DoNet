using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Queries.GetTaskDetails;

public sealed record GetTaskDetailsQuery(Guid TaskId) : IRequest<TaskDetailsDto?>;
