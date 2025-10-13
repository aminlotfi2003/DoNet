using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Projects.DTOs;
using MediatR;

namespace DoNet.Application.Projects.Queries.GetProjectMembers;

internal sealed class GetProjectMembersQueryHandler : IRequestHandler<GetProjectMembersQuery, IReadOnlyCollection<ProjectMemberDto>>
{
    private readonly IProjectRepository _projects;

    public GetProjectMembersQueryHandler(IProjectRepository projects)
    {
        _projects = projects;
    }

    public async Task<IReadOnlyCollection<ProjectMemberDto>> Handle(GetProjectMembersQuery request, CancellationToken ct)
    {
        var project = await _projects.GetByIdAsync(request.ProjectId, ct)
                      ?? throw new InvalidOperationException($"Project {request.ProjectId} was not found.");

        var members = request.IncludeFormerMembers
            ? project.Members
            : project.Members.Where(m => m.LeftAt is null);

        return members.Select(ProjectMemberDto.FromEntity).ToList();
    }
}
