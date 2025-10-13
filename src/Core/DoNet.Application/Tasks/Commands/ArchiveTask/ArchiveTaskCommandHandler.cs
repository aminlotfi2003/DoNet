using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Abstractions.UoW;
using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Commands.ArchiveTask;

internal sealed class ArchiveTaskCommandHandler : IRequestHandler<ArchiveTaskCommand, TaskSummaryDto>
{
    private readonly ITaskItemRepository _tasks;
    private readonly IUnitOfWork _uow;

    public ArchiveTaskCommandHandler(ITaskItemRepository tasks, IUnitOfWork uow)
    {
        _tasks = tasks;
        _uow = uow;
    }

    public async Task<TaskSummaryDto> Handle(ArchiveTaskCommand request, CancellationToken ct)
    {
        var task = await _tasks.GetByIdAsync(request.TaskId, ct)
                   ?? throw new InvalidOperationException($"Task {request.TaskId} was not found.");

        task.Archive();
        _tasks.Update(task);
        await _uow.SaveChangesAsync(ct);

        return TaskSummaryDto.FromEntity(task);
    }
}
