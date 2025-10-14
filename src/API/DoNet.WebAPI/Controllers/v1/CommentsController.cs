using DoNet.Application.Comments.Commands.DeleteComment;
using DoNet.Application.Comments.DTOs;
using DoNet.Application.Tasks.Commands.EditTaskComment;
using DoNet.WebFramework.Contracts.Comments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DoNet.WebAPI.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/comments")]
public sealed class CommentsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    #region Edit Comment
    [HttpPut("{commentId:guid}")]
    public async Task<ActionResult<CommentDto>> EditComment(Guid commentId, EditCommentRequest request)
    {
        try
        {
            var comment = await _mediator.Send(new EditTaskCommentCommand(commentId, request.Body));
            return Ok(comment);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
    #endregion

    #region Delete Comment
    [HttpDelete("{commentId:guid}")]
    public async Task<IActionResult> DeleteComment(Guid commentId)
    {
        try
        {
            await _mediator.Send(new DeleteCommentCommand(commentId));
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
    #endregion
}
