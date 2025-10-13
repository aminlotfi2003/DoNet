using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Abstractions.UoW;
using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Commands.AssignUserToTask;

internal sealed class AssignUserToTaskCommandHandler : IRequestHandler<AssignUserToTaskCommand, TaskAssignmentDto>
{
    private readonly ITaskItemRepository _tasks;
    private readonly IUnitOfWork _uow;

    public AssignUserToTaskCommandHandler(ITaskItemRepository tasks, IUnitOfWork uow)
    {
        _tasks = tasks;
        _uow = uow;
    }

    public async Task<TaskAssignmentDto> Handle(AssignUserToTaskCommand request, CancellationToken ct)
    {
        var task = await _tasks.GetByIdAsync(request.TaskId, ct)
                   ?? throw new InvalidOperationException($"Task {request.TaskId} was not found.");

        task.AssignUser(request.UserId, request.Role);
        _tasks.Update(task);
        await _uow.SaveChangesAsync(ct);

        var assignment = task.Assignees.Single(a => a.UserId == request.UserId);
        return TaskAssignmentDto.FromEntity(assignment);
    }
}
