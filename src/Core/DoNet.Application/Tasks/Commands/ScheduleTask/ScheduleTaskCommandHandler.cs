using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Abstractions.UoW;
using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Commands.ScheduleTask;

internal sealed class ScheduleTaskCommandHandler : IRequestHandler<ScheduleTaskCommand, TaskSummaryDto>
{
    private readonly ITaskItemRepository _tasks;
    private readonly IUnitOfWork _unitOfWork;

    public ScheduleTaskCommandHandler(ITaskItemRepository tasks, IUnitOfWork unitOfWork)
    {
        _tasks = tasks;
        _unitOfWork = unitOfWork;
    }

    public async Task<TaskSummaryDto> Handle(ScheduleTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _tasks.GetByIdAsync(request.TaskId, cancellationToken)
                   ?? throw new InvalidOperationException($"Task {request.TaskId} was not found.");

        task.Schedule(request.DueDate);
        _tasks.Update(task);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return TaskSummaryDto.FromEntity(task);
    }
}
