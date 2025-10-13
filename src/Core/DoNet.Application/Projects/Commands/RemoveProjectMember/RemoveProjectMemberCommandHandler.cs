using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Abstractions.UoW;
using DoNet.Application.Projects.DTOs;
using MediatR;

namespace DoNet.Application.Projects.Commands.RemoveProjectMember;

internal sealed class RemoveProjectMemberCommandHandler : IRequestHandler<RemoveProjectMemberCommand, ProjectMemberDto?>
{
    private readonly IProjectRepository _projects;
    private readonly IUnitOfWork _uow;

    public RemoveProjectMemberCommandHandler(IProjectRepository projects, IUnitOfWork uow)
    {
        _projects = projects;
        _uow = uow;
    }

    public async Task<ProjectMemberDto?> Handle(RemoveProjectMemberCommand request, CancellationToken ct)
    {
        var project = await _projects.GetByIdAsync(request.ProjectId, ct)
                      ?? throw new InvalidOperationException($"Project {request.ProjectId} was not found.");

        var membership = project.Members.FirstOrDefault(m => m.UserId == request.UserId && m.LeftAt is null);
        if (membership is null)
            return null;

        project.RemoveMember(request.UserId);
        _projects.Update(project);
        await _uow.SaveChangesAsync(ct);

        return ProjectMemberDto.FromEntity(membership);
    }
}
