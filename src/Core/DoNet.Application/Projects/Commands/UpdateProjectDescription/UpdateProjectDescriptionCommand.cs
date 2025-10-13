using DoNet.Application.Projects.DTOs;
using MediatR;

namespace DoNet.Application.Projects.Commands.UpdateProjectDescription;

public sealed record UpdateProjectDescriptionCommand(Guid ProjectId, string? Description) : IRequest<ProjectDto>;
