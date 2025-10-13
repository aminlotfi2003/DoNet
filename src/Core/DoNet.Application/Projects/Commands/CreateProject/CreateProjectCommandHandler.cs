using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Abstractions.UoW;
using DoNet.Application.Projects.DTOs;
using DoNet.Domain.Entities;
using MediatR;

namespace DoNet.Application.Projects.Commands.CreateProject;

internal sealed class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IProjectRepository _projects;
    private readonly IUnitOfWork _uow;

    public CreateProjectCommandHandler(IProjectRepository projects, IUnitOfWork uow)
    {
        _projects = projects;
        _uow = uow;
    }

    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken ct)
    {
        var project = Project.Create(request.Name, request.Description);
        await _projects.AddAsync(project, ct);
        await _uow.SaveChangesAsync(ct);
        return ProjectDto.FromEntity(project);
    }
}
