using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Queries.GetTaskById;

internal sealed class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskSummaryDto?>
{
    private readonly ITaskItemRepository _tasks;

    public GetTaskByIdQueryHandler(ITaskItemRepository tasks)
    {
        _tasks = tasks;
    }

    public async Task<TaskSummaryDto?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _tasks.GetByIdAsync(request.TaskId, cancellationToken);
        return task is null ? null : TaskSummaryDto.FromEntity(task);
    }
}
