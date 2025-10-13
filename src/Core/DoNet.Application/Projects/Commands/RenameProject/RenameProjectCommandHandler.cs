using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Abstractions.UoW;
using DoNet.Application.Projects.DTOs;
using MediatR;

namespace DoNet.Application.Projects.Commands.RenameProject;

internal sealed class RenameProjectCommandHandler : IRequestHandler<RenameProjectCommand, ProjectDto>
{
    private readonly IProjectRepository _projects;
    private readonly IUnitOfWork _uow;

    public RenameProjectCommandHandler(IProjectRepository projects, IUnitOfWork uow)
    {
        _projects = projects;
        _uow = uow;
    }

    public async Task<ProjectDto> Handle(RenameProjectCommand request, CancellationToken ct)
    {
        var project = await _projects.GetByIdAsync(request.ProjectId, ct)
                      ?? throw new InvalidOperationException($"Project {request.ProjectId} was not found.");

        project.Rename(request.Name);
        _projects.Update(project);
        await _uow.SaveChangesAsync(ct);

        return ProjectDto.FromEntity(project);
    }
}
