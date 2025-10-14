namespace DoNet.WebFramework.Contracts.Tasks;

public sealed record AddTaskCommentRequest(Guid AuthorId, string Body);
