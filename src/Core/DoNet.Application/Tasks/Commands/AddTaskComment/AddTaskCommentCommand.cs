using DoNet.Application.Comments.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Commands.AddTaskComment;

public sealed record AddTaskCommentCommand(Guid TaskId, Guid AuthorId, string Body) : IRequest<CommentDto>;
