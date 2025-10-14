using DoNet.Application.Projects.Commands.AddProjectMember;
using DoNet.Application.Projects.Commands.ArchiveProject;
using DoNet.Application.Projects.Commands.CreateProject;
using DoNet.Application.Projects.Commands.RemoveProjectMember;
using DoNet.Application.Projects.Commands.RenameProject;
using DoNet.Application.Projects.Commands.UpdateProjectDescription;
using DoNet.Application.Projects.DTOs;
using DoNet.Application.Projects.Queries.GetProjectById;
using DoNet.Application.Projects.Queries.GetProjectDetails;
using DoNet.Application.Projects.Queries.GetProjectMembers;
using DoNet.Application.Projects.Queries.ListProjects;
using DoNet.WebFramework.Contracts.Projects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DoNet.WebAPI.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/projects")]
public sealed class ProjectsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    #region List Projects
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<ProjectDto>>> ListProjects([FromQuery] bool includeArchived = false)
    {
        var projects = await _mediator.Send(new ListProjectsQuery(includeArchived));
        return Ok(projects);
    }
    #endregion

    #region Get Project By Id
    [HttpGet("{projectId:guid}")]
    public async Task<ActionResult<ProjectDto>> GetProject(Guid projectId)
    {
        var project = await _mediator.Send(new GetProjectByIdQuery(projectId));
        return project is null ? NotFound() : Ok(project);
    }
    #endregion

    #region Get Project Details
    [HttpGet("{projectId:guid}/details")]
    public async Task<ActionResult<ProjectDetailsDto>> GetProjectDetails(Guid projectId)
    {
        var details = await _mediator.Send(new GetProjectDetailsQuery(projectId));
        return details is null ? NotFound() : Ok(details);
    }
    #endregion

    #region Get Project Members
    [HttpGet("{projectId:guid}/members")]
    public async Task<ActionResult<IReadOnlyCollection<ProjectMemberDto>>> GetProjectMembers(
        Guid projectId,
        [FromQuery] bool includeFormerMembers = false)
    {
        try
        {
            var members = await _mediator.Send(new GetProjectMembersQuery(projectId, includeFormerMembers));
            return Ok(members);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
    #endregion

    #region Create Project
    [HttpPost]
    public async Task<ActionResult<ProjectDto>> CreateProject(CreateProjectRequest request)
    {
        var project = await _mediator.Send(new CreateProjectCommand(request.Name, request.Description));
        return CreatedAtAction(nameof(GetProject), new { projectId = project.Id }, project);
    }
    #endregion

    #region Rename Project
    [HttpPut("{projectId:guid}/name")]
    public async Task<ActionResult<ProjectDto>> RenameProject(Guid projectId, RenameProjectRequest request)
    {
        try
        {
            var project = await _mediator.Send(new RenameProjectCommand(projectId, request.Name));
            return Ok(project);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
    #endregion

    #region Update Project Description
    [HttpPut("{projectId:guid}/description")]
    public async Task<ActionResult<ProjectDto>> UpdateProjectDescription(Guid projectId, UpdateProjectDescriptionRequest request)
    {
        try
        {
            var project = await _mediator.Send(new UpdateProjectDescriptionCommand(projectId, request.Description));
            return Ok(project);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
    #endregion

    #region Archive Project
    [HttpPost("{projectId:guid}/archive")]
    public async Task<ActionResult<ProjectDto>> ArchiveProject(Guid projectId)
    {
        try
        {
            var project = await _mediator.Send(new ArchiveProjectCommand(projectId));
            return Ok(project);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
    #endregion

    #region Add Project Member
    [HttpPost("{projectId:guid}/members")]
    public async Task<ActionResult<ProjectMemberDto>> AddProjectMember(Guid projectId, AddProjectMemberRequest request)
    {
        try
        {
            var member = await _mediator.Send(new AddProjectMemberCommand(projectId, request.UserId, request.Role));
            return Ok(member);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
    #endregion

    #region Remove Project Member
    [HttpDelete("{projectId:guid}/members/{userId:guid}")]
    public async Task<ActionResult<ProjectMemberDto>> RemoveProjectMember(Guid projectId, Guid userId)
    {
        try
        {
            var member = await _mediator.Send(new RemoveProjectMemberCommand(projectId, userId));
            return member is null ? NotFound() : Ok(member);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
    #endregion
}
