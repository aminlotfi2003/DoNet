using DoNet.Application.Projects.DTOs;
using MediatR;

namespace DoNet.Application.Projects.Queries.GetProjectMembers;

public sealed record GetProjectMembersQuery(Guid ProjectId, bool IncludeFormerMembers = false) : IRequest<IReadOnlyCollection<ProjectMemberDto>>;
