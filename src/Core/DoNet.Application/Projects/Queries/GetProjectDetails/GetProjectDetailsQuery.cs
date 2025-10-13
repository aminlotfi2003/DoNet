using DoNet.Application.Projects.DTOs;
using MediatR;

namespace DoNet.Application.Projects.Queries.GetProjectDetails;

public sealed record GetProjectDetailsQuery(Guid ProjectId) : IRequest<ProjectDetailsDto?>;
