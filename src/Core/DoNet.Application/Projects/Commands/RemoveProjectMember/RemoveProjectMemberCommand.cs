using DoNet.Application.Projects.DTOs;
using MediatR;

namespace DoNet.Application.Projects.Commands.RemoveProjectMember;

public sealed record RemoveProjectMemberCommand(Guid ProjectId, Guid UserId) : IRequest<ProjectMemberDto?>;
