using DoNet.SharedKernel.Abstractions.Domain;

namespace DoNet.Domain.Entities;

public class Comment : EntityBase
{
    public string Body { get; set; } = default!;
    public Guid TaskItemId { get; set; }
    public Guid AuthorId { get; set; }

    private Comment() { } // For EF

    public Comment(Guid taskItemId, Guid authorId, string body)
    {
        TaskItemId = taskItemId != Guid.Empty
            ? taskItemId
            : throw new ArgumentException("TaskItemId is required.", nameof(taskItemId));
        AuthorId = authorId != Guid.Empty
            ? authorId
            : throw new ArgumentException("AuthorId is required.", nameof(authorId));
        Body = GuardBody(body);
    }

    public static Comment Create(Guid taskItemId, Guid authorId, string body) 
        => new(taskItemId, authorId, body);

    public void Edit(string body)
    {
        Body = GuardBody(body);
        Touch();
    }

    #region Guard Body
    private static string GuardBody(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
            throw new ArgumentException("Comment body is required.", nameof(body));
        var b = body.Trim();
        if (b.Length > 4000)
            throw new ArgumentOutOfRangeException(nameof(body), "Comment max length is 4000.");
        return b;
    }
    #endregion
}
