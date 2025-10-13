using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Abstractions.UoW;
using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Commands.RenameTask;

internal sealed class RenameTaskCommandHandler : IRequestHandler<RenameTaskCommand, TaskSummaryDto>
{
    private readonly ITaskItemRepository _tasks;
    private readonly IUnitOfWork _uow;

    public RenameTaskCommandHandler(ITaskItemRepository tasks, IUnitOfWork uow)
    {
        _tasks = tasks;
        _uow = uow;
    }

    public async Task<TaskSummaryDto> Handle(RenameTaskCommand request, CancellationToken ct)
    {
        var task = await _tasks.GetByIdAsync(request.TaskId, ct)
                   ?? throw new InvalidOperationException($"Task {request.TaskId} was not found.");

        task.Rename(request.Title);
        _tasks.Update(task);
        await _uow.SaveChangesAsync(ct);

        return TaskSummaryDto.FromEntity(task);
    }
}
