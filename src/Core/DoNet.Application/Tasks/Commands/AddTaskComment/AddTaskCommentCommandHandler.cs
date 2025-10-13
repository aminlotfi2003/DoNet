using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Abstractions.UoW;
using DoNet.Application.Comments.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Commands.AddTaskComment;

internal sealed class AddTaskCommentCommandHandler : IRequestHandler<AddTaskCommentCommand, CommentDto>
{
    private readonly ITaskItemRepository _tasks;
    private readonly ICommentRepository _comments;
    private readonly IUnitOfWork _uow;

    public AddTaskCommentCommandHandler(ITaskItemRepository tasks, ICommentRepository comments, IUnitOfWork uow)
    {
        _tasks = tasks;
        _comments = comments;
        _uow = uow;
    }

    public async Task<CommentDto> Handle(AddTaskCommentCommand request, CancellationToken ct)
    {
        var task = await _tasks.GetByIdAsync(request.TaskId, ct)
                   ?? throw new InvalidOperationException($"Task {request.TaskId} was not found.");

        var comment = task.AddComment(request.AuthorId, request.Body);
        await _comments.AddAsync(comment, ct);
        _tasks.Update(task);
        await _uow.SaveChangesAsync(ct);

        return CommentDto.FromEntity(comment);
    }
}
