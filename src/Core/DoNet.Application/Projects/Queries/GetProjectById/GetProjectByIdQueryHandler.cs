using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Projects.DTOs;
using MediatR;

namespace DoNet.Application.Projects.Queries.GetProjectById;


internal sealed class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto?>
{
    private readonly IProjectRepository _projects;

    public GetProjectByIdQueryHandler(IProjectRepository projects)
    {
        _projects = projects;
    }

    public async Task<ProjectDto?> Handle(GetProjectByIdQuery request, CancellationToken ct)
    {
        var project = await _projects.GetByIdAsync(request.ProjectId, ct);
        return project is null ? null : ProjectDto.FromEntity(project);
    }
}
