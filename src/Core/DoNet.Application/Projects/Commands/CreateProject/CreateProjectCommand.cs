using DoNet.Application.Projects.DTOs;
using MediatR;

namespace DoNet.Application.Projects.Commands.CreateProject;

public sealed record CreateProjectCommand(string Name, string? Description) : IRequest<ProjectDto>;
