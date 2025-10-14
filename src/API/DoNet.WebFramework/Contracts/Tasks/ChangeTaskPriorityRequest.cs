using Enum = DoNet.Domain.Entities.Enum;

namespace DoNet.WebFramework.Contracts.Tasks;

public sealed record ChangeTaskPriorityRequest(Enum.TaskPriority Priority);
