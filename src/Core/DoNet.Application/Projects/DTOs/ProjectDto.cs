using DoNet.Domain.Entities;

namespace DoNet.Application.Projects.DTOs;

public sealed record ProjectDto(
    Guid Id,
    string Name,
    string? Description,
    bool IsArchived,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt)
{
    public static ProjectDto FromEntity(Project project)
        => new(
            project.Id,
            project.Name,
            project.Description,
            project.IsArchived,
            project.CreatedAt,
            project.UpdatedAt);
}
