using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Queries.GetTaskAssignments;

internal sealed class GetTaskAssignmentsQueryHandler : IRequestHandler<GetTaskAssignmentsQuery, IReadOnlyCollection<TaskAssignmentDto>>
{
    private readonly ITaskItemRepository _tasks;

    public GetTaskAssignmentsQueryHandler(ITaskItemRepository tasks)
    {
        _tasks = tasks;
    }

    public async Task<IReadOnlyCollection<TaskAssignmentDto>> Handle(GetTaskAssignmentsQuery request, CancellationToken cancellationToken)
    {
        var task = await _tasks.GetByIdAsync(request.TaskId, cancellationToken)
                   ?? throw new InvalidOperationException($"Task {request.TaskId} was not found.");

        return task.Assignees.Select(TaskAssignmentDto.FromEntity).ToList();
    }
}
