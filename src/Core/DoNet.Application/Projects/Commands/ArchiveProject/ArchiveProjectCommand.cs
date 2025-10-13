using DoNet.Application.Projects.DTOs;
using MediatR;

namespace DoNet.Application.Projects.Commands.ArchiveProject;

public sealed record ArchiveProjectCommand(Guid ProjectId) : IRequest<ProjectDto>;
