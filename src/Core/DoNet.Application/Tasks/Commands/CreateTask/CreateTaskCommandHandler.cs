using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Abstractions.UoW;
using DoNet.Application.Tasks.DTOs;
using MediatR;

namespace DoNet.Application.Tasks.Commands.CreateTask;

internal sealed class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskSummaryDto>
{
    private readonly IProjectRepository _projects;
    private readonly ITaskItemRepository _tasks;
    private readonly IUnitOfWork _uow;

    public CreateTaskCommandHandler(IProjectRepository projects, ITaskItemRepository tasks, IUnitOfWork uow)
    {
        _projects = projects;
        _tasks = tasks;
        _uow = uow;
    }

    public async Task<TaskSummaryDto> Handle(CreateTaskCommand request, CancellationToken ct)
    {
        var project = await _projects.GetByIdAsync(request.ProjectId, ct)
                      ?? throw new InvalidOperationException($"Project {request.ProjectId} was not found.");

        var task = project.AddTask(request.Title, request.Description, request.Priority, request.DueDate);
        await _tasks.AddAsync(task, ct);
        _projects.Update(project);
        await _uow.SaveChangesAsync(ct);

        return TaskSummaryDto.FromEntity(task);
    }
}
