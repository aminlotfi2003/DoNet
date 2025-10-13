using DoNet.Application.Comments.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Commands.EditTaskComment;

public sealed record EditTaskCommentCommand(Guid CommentId, string Body) : IRequest<CommentDto>;
