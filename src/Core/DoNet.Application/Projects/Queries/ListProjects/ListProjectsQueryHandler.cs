using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Projects.DTOs;
using MediatR;

namespace DoNet.Application.Projects.Queries.ListProjects;

internal sealed class ListProjectsQueryHandler : IRequestHandler<ListProjectsQuery, IReadOnlyCollection<ProjectDto>>
{
    private readonly IProjectRepository _projects;

    public ListProjectsQueryHandler(IProjectRepository projects)
    {
        _projects = projects;
    }

    public async Task<IReadOnlyCollection<ProjectDto>> Handle(ListProjectsQuery request, CancellationToken ct)
    {
        var projects = request.IncludeArchived
            ? await _projects.ListAsync(ct)
            : await _projects.ListAsync(p => !p.IsArchived, ct);

        return projects.Select(ProjectDto.FromEntity).ToList();
    }
}
