using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Queries.GetTasksByProject;

internal sealed class GetTasksByProjectQueryHandler : IRequestHandler<GetTasksByProjectQuery, IReadOnlyCollection<TaskSummaryDto>>
{
    private readonly ITaskItemRepository _tasks;

    public GetTasksByProjectQueryHandler(ITaskItemRepository tasks)
    {
        _tasks = tasks;
    }

    public async Task<IReadOnlyCollection<TaskSummaryDto>> Handle(GetTasksByProjectQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _tasks.ListAsync(t => t.ProjectId == request.ProjectId && (request.IncludeArchived || !t.IsArchived), cancellationToken);
        return tasks.Select(TaskSummaryDto.FromEntity).ToList();
    }
}
