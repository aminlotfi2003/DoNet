using DoNet.Application.Tasks.DTOs;
using DoNet.Domain.Entities;

namespace DoNet.Application.Projects.DTOs;

public sealed record ProjectDetailsDto(
    ProjectDto Project,
    IReadOnlyCollection<TaskSummaryDto> Tasks,
    IReadOnlyCollection<ProjectMemberDto> Members)
{
    public static ProjectDetailsDto FromEntity(Project project)
        => new(
            ProjectDto.FromEntity(project),
            project.Tasks.Select(TaskSummaryDto.FromEntity).ToList(),
            project.Members.Select(ProjectMemberDto.FromEntity).ToList());
}
