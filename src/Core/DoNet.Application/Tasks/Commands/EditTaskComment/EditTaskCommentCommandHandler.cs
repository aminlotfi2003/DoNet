using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Abstractions.UoW;
using DoNet.Application.Comments.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Commands.EditTaskComment;

internal sealed class EditTaskCommentCommandHandler : IRequestHandler<EditTaskCommentCommand, CommentDto>
{
    private readonly ICommentRepository _comments;
    private readonly IUnitOfWork _uow;

    public EditTaskCommentCommandHandler(ICommentRepository comments, IUnitOfWork uow)
    {
        _comments = comments;
        _uow = uow;
    }

    public async Task<CommentDto> Handle(EditTaskCommentCommand request, CancellationToken ct)
    {
        var comment = await _comments.GetByIdAsync(request.CommentId, ct)
                      ?? throw new InvalidOperationException($"Comment {request.CommentId} was not found.");

        comment.Edit(request.Body);
        _comments.Update(comment);
        await _uow.SaveChangesAsync(ct);

        return CommentDto.FromEntity(comment);
    }
}
