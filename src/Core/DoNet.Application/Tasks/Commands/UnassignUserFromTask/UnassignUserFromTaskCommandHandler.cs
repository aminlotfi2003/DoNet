using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Abstractions.UoW;
using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Commands.UnassignUserFromTask;

internal sealed class UnassignUserFromTaskCommandHandler : IRequestHandler<UnassignUserFromTaskCommand, TaskAssignmentDto?>
{
    private readonly ITaskItemRepository _tasks;
    private readonly IUnitOfWork _unitOfWork;

    public UnassignUserFromTaskCommandHandler(ITaskItemRepository tasks, IUnitOfWork unitOfWork)
    {
        _tasks = tasks;
        _unitOfWork = unitOfWork;
    }

    public async Task<TaskAssignmentDto?> Handle(UnassignUserFromTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _tasks.GetByIdAsync(request.TaskId, cancellationToken)
                   ?? throw new InvalidOperationException($"Task {request.TaskId} was not found.");

        var assignment = task.Assignees.FirstOrDefault(a => a.UserId == request.UserId);
        if (assignment is null)
        {
            return null;
        }

        task.UnassignUser(request.UserId);
        _tasks.Update(task);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return TaskAssignmentDto.FromEntity(assignment);
    }
}
