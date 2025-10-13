using DoNet.Application.Projects.DTOs;
using MediatR;

namespace DoNet.Application.Projects.Commands.RenameProject;

public sealed record RenameProjectCommand(Guid ProjectId, string Name) : IRequest<ProjectDto>;
