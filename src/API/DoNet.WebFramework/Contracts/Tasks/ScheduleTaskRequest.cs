namespace DoNet.WebFramework.Contracts.Tasks;

public sealed record ScheduleTaskRequest(DateTimeOffset? DueDate);
