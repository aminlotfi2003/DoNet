using DoNet.Domain.Entities;
using Enum = DoNet.Domain.Entities.Enum;

namespace DoNet.Application.Projects.DTOs;

public sealed record ProjectMemberDto(
    Guid UserId,
    Guid ProjectId,
    Enum.ProjectRole Role,
    DateTimeOffset JoinedAt,
    DateTimeOffset? LeftAt)
{
    public static ProjectMemberDto FromEntity(UserProjectMembership membership)
        => new(
            membership.UserId,
            membership.ProjectId,
            membership.Role,
            membership.JoinedAt,
            membership.LeftAt);
}
