using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Abstractions.UoW;
using DoNet.Application.Projects.DTOs;
using MediatR;

namespace DoNet.Application.Projects.Commands.ArchiveProject;

internal sealed class ArchiveProjectCommandHandler : IRequestHandler<ArchiveProjectCommand, ProjectDto>
{
    private readonly IProjectRepository _projects;
    private readonly IUnitOfWork _uow;

    public ArchiveProjectCommandHandler(IProjectRepository projects, IUnitOfWork uow)
    {
        _projects = projects;
        _uow = uow;
    }

    public async Task<ProjectDto> Handle(ArchiveProjectCommand request, CancellationToken ct)
    {
        var project = await _projects.GetByIdAsync(request.ProjectId, ct)
                      ?? throw new InvalidOperationException($"Project {request.ProjectId} was not found.");

        project.Archive();
        _projects.Update(project);
        await _uow.SaveChangesAsync(ct);

        return ProjectDto.FromEntity(project);
    }
}
