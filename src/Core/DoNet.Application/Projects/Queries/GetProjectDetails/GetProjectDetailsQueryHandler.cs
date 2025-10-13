using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Projects.DTOs;
using MediatR;

namespace DoNet.Application.Projects.Queries.GetProjectDetails;

internal sealed class GetProjectDetailsQueryHandler : IRequestHandler<GetProjectDetailsQuery, ProjectDetailsDto?>
{
    private readonly IProjectRepository _projects;

    public GetProjectDetailsQueryHandler(IProjectRepository projects)
    {
        _projects = projects;
    }

    public async Task<ProjectDetailsDto?> Handle(GetProjectDetailsQuery request, CancellationToken ct)
    {
        var project = await _projects.GetByIdAsync(request.ProjectId, ct);
        return project is null ? null : ProjectDetailsDto.FromEntity(project);
    }
}
