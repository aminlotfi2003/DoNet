using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Abstractions.UoW;
using DoNet.Application.Projects.DTOs;
using MediatR;

namespace DoNet.Application.Projects.Commands.AddProjectMember;

internal sealed class AddProjectMemberCommandHandler : IRequestHandler<AddProjectMemberCommand, ProjectMemberDto>
{
    private readonly IProjectRepository _projects;
    private readonly IUnitOfWork _uow;

    public AddProjectMemberCommandHandler(IProjectRepository projects, IUnitOfWork uow)
    {
        _projects = projects;
        _uow = uow;
    }

    public async Task<ProjectMemberDto> Handle(AddProjectMemberCommand request, CancellationToken ct)
    {
        var project = await _projects.GetByIdAsync(request.ProjectId, ct)
                      ?? throw new InvalidOperationException($"Project {request.ProjectId} was not found.");

        project.AddMember(request.UserId, request.Role);
        _projects.Update(project);
        await _uow.SaveChangesAsync(ct);

        var membership = project.Members.Single(m => m.UserId == request.UserId && m.LeftAt is null);
        return ProjectMemberDto.FromEntity(membership);
    }
}
