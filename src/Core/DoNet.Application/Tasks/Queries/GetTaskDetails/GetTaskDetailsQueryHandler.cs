using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Queries.GetTaskDetails;

internal sealed class GetTaskDetailsQueryHandler : IRequestHandler<GetTaskDetailsQuery, TaskDetailsDto?>
{
    private readonly ITaskItemRepository _tasks;

    public GetTaskDetailsQueryHandler(ITaskItemRepository tasks)
    {
        _tasks = tasks;
    }

    public async Task<TaskDetailsDto?> Handle(GetTaskDetailsQuery request, CancellationToken cancellationToken)
    {
        var task = await _tasks.GetByIdAsync(request.TaskId, cancellationToken);
        return task is null ? null : TaskDetailsDto.FromEntity(task);
    }
}
