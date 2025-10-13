using MediatR;

namespace DoNet.Application.Comments.Commands.DeleteComment;

public sealed record DeleteCommentCommand(Guid CommentId) : IRequest<Unit>;
