using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Abstractions.UoW;
using MediatR;

namespace DoNet.Application.Comments.Commands.DeleteComment;

internal sealed class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Unit>
{
    private readonly ICommentRepository _comments;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCommentCommandHandler(ICommentRepository comments, IUnitOfWork unitOfWork)
    {
        _comments = comments;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _comments.GetByIdAsync(request.CommentId, cancellationToken)
                      ?? throw new InvalidOperationException($"Comment {request.CommentId} was not found.");

        _comments.Remove(comment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
