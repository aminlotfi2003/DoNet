using DoNet.Domain.Entities;

namespace DoNet.Application.Comments.DTOs;

public sealed record CommentDto(
    Guid Id,
    Guid TaskId,
    Guid AuthorId,
    string Body,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt)
{
    public static CommentDto FromEntity(Comment comment)
        => new(
            comment.Id,
            comment.TaskItemId,
            comment.AuthorId,
            comment.Body,
            comment.CreatedAt,
            comment.UpdatedAt);
}
