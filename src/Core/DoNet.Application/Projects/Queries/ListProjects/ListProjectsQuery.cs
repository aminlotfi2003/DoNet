using DoNet.Application.Projects.DTOs;
using MediatR;

namespace DoNet.Application.Projects.Queries.ListProjects;


public sealed record ListProjectsQuery(bool IncludeArchived = false) : IRequest<IReadOnlyCollection<ProjectDto>>;
