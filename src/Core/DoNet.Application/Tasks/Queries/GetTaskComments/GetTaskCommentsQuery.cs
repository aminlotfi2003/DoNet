using DoNet.Application.Comments.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Queries.GetTaskComments;

public sealed record GetTaskCommentsQuery(Guid TaskId) : IRequest<IReadOnlyCollection<CommentDto>>;
