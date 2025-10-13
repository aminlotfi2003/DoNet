using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Abstractions.UoW;
using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Commands.ChangeTaskStatus;

internal sealed class ChangeTaskStatusCommandHandler : IRequestHandler<ChangeTaskStatusCommand, TaskSummaryDto>
{
    private readonly ITaskItemRepository _tasks;
    private readonly IUnitOfWork _uow;

    public ChangeTaskStatusCommandHandler(ITaskItemRepository tasks, IUnitOfWork uow)
    {
        _tasks = tasks;
        _uow = uow;
    }

    public async Task<TaskSummaryDto> Handle(ChangeTaskStatusCommand request, CancellationToken ct)
    {
        var task = await _tasks.GetByIdAsync(request.TaskId, ct)
                   ?? throw new InvalidOperationException($"Task {request.TaskId} was not found.");

        task.ChangeStatus(request.Status);
        _tasks.Update(task);
        await _uow.SaveChangesAsync(ct);

        return TaskSummaryDto.FromEntity(task);
    }
}
