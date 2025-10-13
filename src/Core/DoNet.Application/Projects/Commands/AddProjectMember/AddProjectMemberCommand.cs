using DoNet.Application.Projects.DTOs;
using MediatR;
using Enum = DoNet.Domain.Entities.Enum;

namespace DoNet.Application.Projects.Commands.AddProjectMember;

public sealed record AddProjectMemberCommand(Guid ProjectId, Guid UserId, Enum.ProjectRole Role) : IRequest<ProjectMemberDto>;
