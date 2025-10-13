using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Comments.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Queries.GetTaskComments;

internal sealed class GetTaskCommentsQueryHandler : IRequestHandler<GetTaskCommentsQuery, IReadOnlyCollection<CommentDto>>
{
    private readonly ITaskItemRepository _tasks;

    public GetTaskCommentsQueryHandler(ITaskItemRepository tasks)
    {
        _tasks = tasks;
    }

    public async Task<IReadOnlyCollection<CommentDto>> Handle(GetTaskCommentsQuery request, CancellationToken cancellationToken)
    {
        var task = await _tasks.GetByIdAsync(request.TaskId, cancellationToken)
                   ?? throw new InvalidOperationException($"Task {request.TaskId} was not found.");

        return task.Comments.Select(CommentDto.FromEntity).ToList();
    }
}
