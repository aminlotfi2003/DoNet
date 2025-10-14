using Enum = DoNet.Domain.Entities.Enum;

namespace DoNet.WebFramework.Contracts.Tasks;

public sealed record AssignUserToTaskRequest(Guid UserId, Enum.TaskAssignmentRole Role);
