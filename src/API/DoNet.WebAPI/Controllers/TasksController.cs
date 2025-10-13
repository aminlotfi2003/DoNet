using DoNet.Application.Comments.DTOs;
using DoNet.Application.Tasks.Commands.AddTaskComment;
using DoNet.Application.Tasks.Commands.ArchiveTask;
using DoNet.Application.Tasks.Commands.AssignUserToTask;
using DoNet.Application.Tasks.Commands.ChangeTaskPriority;
using DoNet.Application.Tasks.Commands.ChangeTaskStatus;
using DoNet.Application.Tasks.Commands.CreateTask;
using DoNet.Application.Tasks.Commands.RenameTask;
using DoNet.Application.Tasks.Commands.ScheduleTask;
using DoNet.Application.Tasks.Commands.UnassignUserFromTask;
using DoNet.Application.Tasks.Commands.UpdateTaskDescription;
using DoNet.Application.Tasks.DTOs;
using DoNet.Application.Tasks.Queries.GetTaskAssignments;
using DoNet.Application.Tasks.Queries.GetTaskById;
using DoNet.Application.Tasks.Queries.GetTaskComments;
using DoNet.Application.Tasks.Queries.GetTaskDetails;
using DoNet.Application.Tasks.Queries.GetTasksByProject;
using Enum = DoNet.Domain.Entities.Enum;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DoNet.WebAPI.Controllers;

[ApiController]
[Route("api/tasks")]
public sealed class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{taskId:guid}")]
    public async Task<ActionResult<TaskSummaryDto>> GetTask(Guid taskId)
    {
        var task = await _mediator.Send(new GetTaskByIdQuery(taskId));
        return task is null ? NotFound() : Ok(task);
    }

    [HttpGet("{taskId:guid}/details")]
    public async Task<ActionResult<TaskDetailsDto>> GetTaskDetails(Guid taskId)
    {
        var details = await _mediator.Send(new GetTaskDetailsQuery(taskId));
        return details is null ? NotFound() : Ok(details);
    }

    [HttpGet("{taskId:guid}/assignments")]
    public async Task<ActionResult<IReadOnlyCollection<TaskAssignmentDto>>> GetTaskAssignments(Guid taskId)
    {
        try
        {
            var assignments = await _mediator.Send(new GetTaskAssignmentsQuery(taskId));
            return Ok(assignments);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpGet("{taskId:guid}/comments")]
    public async Task<ActionResult<IReadOnlyCollection<CommentDto>>> GetTaskComments(Guid taskId)
    {
        try
        {
            var comments = await _mediator.Send(new GetTaskCommentsQuery(taskId));
            return Ok(comments);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpGet("by-project/{projectId:guid}")]
    public async Task<ActionResult<IReadOnlyCollection<TaskSummaryDto>>> GetTasksByProject(
        Guid projectId,
        [FromQuery] bool includeArchived = false)
    {
        var tasks = await _mediator.Send(new GetTasksByProjectQuery(projectId, includeArchived));
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<ActionResult<TaskSummaryDto>> CreateTask(CreateTaskRequest request)
    {
        try
        {
            var task = await _mediator.Send(new CreateTaskCommand(request.ProjectId, request.Title, request.Description, request.Priority, request.DueDate));
            return CreatedAtAction(nameof(GetTask), new { taskId = task.Id }, task);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPut("{taskId:guid}/name")]
    public async Task<ActionResult<TaskSummaryDto>> RenameTask(Guid taskId, RenameTaskRequest request)
    {
        try
        {
            var task = await _mediator.Send(new RenameTaskCommand(taskId, request.Title));
            return Ok(task);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPut("{taskId:guid}/description")]
    public async Task<ActionResult<TaskSummaryDto>> UpdateTaskDescription(Guid taskId, UpdateTaskDescriptionRequest request)
    {
        try
        {
            var task = await _mediator.Send(new UpdateTaskDescriptionCommand(taskId, request.Description));
            return Ok(task);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPut("{taskId:guid}/status")]
    public async Task<ActionResult<TaskSummaryDto>> ChangeTaskStatus(Guid taskId, ChangeTaskStatusRequest request)
    {
        try
        {
            var task = await _mediator.Send(new ChangeTaskStatusCommand(taskId, request.Status));
            return Ok(task);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPut("{taskId:guid}/priority")]
    public async Task<ActionResult<TaskSummaryDto>> ChangeTaskPriority(Guid taskId, ChangeTaskPriorityRequest request)
    {
        try
        {
            var task = await _mediator.Send(new ChangeTaskPriorityCommand(taskId, request.Priority));
            return Ok(task);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPut("{taskId:guid}/schedule")]
    public async Task<ActionResult<TaskSummaryDto>> ScheduleTask(Guid taskId, ScheduleTaskRequest request)
    {
        try
        {
            var task = await _mediator.Send(new ScheduleTaskCommand(taskId, request.DueDate));
            return Ok(task);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost("{taskId:guid}/assignees")]
    public async Task<ActionResult<TaskAssignmentDto>> AssignUserToTask(Guid taskId, AssignUserToTaskRequest request)
    {
        try
        {
            var assignment = await _mediator.Send(new AssignUserToTaskCommand(taskId, request.UserId, request.Role));
            return Ok(assignment);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{taskId:guid}/assignees/{userId:guid}")]
    public async Task<ActionResult<TaskAssignmentDto>> UnassignUserFromTask(Guid taskId, Guid userId)
    {
        try
        {
            var assignment = await _mediator.Send(new UnassignUserFromTaskCommand(taskId, userId));
            return assignment is null ? NotFound() : Ok(assignment);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost("{taskId:guid}/comments")]
    public async Task<ActionResult<CommentDto>> AddComment(Guid taskId, AddTaskCommentRequest request)
    {
        try
        {
            var comment = await _mediator.Send(new AddTaskCommentCommand(taskId, request.AuthorId, request.Body));
            return Ok(comment);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost("{taskId:guid}/archive")]
    public async Task<ActionResult<TaskSummaryDto>> ArchiveTask(Guid taskId)
    {
        try
        {
            var task = await _mediator.Send(new ArchiveTaskCommand(taskId));
            return Ok(task);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    public sealed record CreateTaskRequest(
        Guid ProjectId,
        string Title,
        string? Description,
        Enum.TaskPriority Priority,
        DateTimeOffset? DueDate);

    public sealed record RenameTaskRequest(string Title);

    public sealed record UpdateTaskDescriptionRequest(string? Description);

    public sealed record ChangeTaskStatusRequest(Enum.TaskStatus Status);

    public sealed record ChangeTaskPriorityRequest(Enum.TaskPriority Priority);

    public sealed record ScheduleTaskRequest(DateTimeOffset? DueDate);

    public sealed record AssignUserToTaskRequest(Guid UserId, Enum.TaskAssignmentRole Role);

    public sealed record AddTaskCommentRequest(Guid AuthorId, string Body);
}
