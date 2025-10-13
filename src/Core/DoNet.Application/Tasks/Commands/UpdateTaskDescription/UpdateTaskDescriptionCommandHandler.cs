using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Abstractions.UoW;
using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Commands.UpdateTaskDescription;

internal sealed class UpdateTaskDescriptionCommandHandler : IRequestHandler<UpdateTaskDescriptionCommand, TaskSummaryDto>
{
    private readonly ITaskItemRepository _tasks;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTaskDescriptionCommandHandler(ITaskItemRepository tasks, IUnitOfWork unitOfWork)
    {
        _tasks = tasks;
        _unitOfWork = unitOfWork;
    }

    public async Task<TaskSummaryDto> Handle(UpdateTaskDescriptionCommand request, CancellationToken cancellationToken)
    {
        var task = await _tasks.GetByIdAsync(request.TaskId, cancellationToken)
                   ?? throw new InvalidOperationException($"Task {request.TaskId} was not found.");

        task.ChangeDescription(request.Description);
        _tasks.Update(task);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return TaskSummaryDto.FromEntity(task);
    }
}
