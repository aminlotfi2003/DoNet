using Enum = DoNet.Domain.Entities.Enum;

namespace DoNet.WebFramework.Contracts.Projects;

public sealed record AddProjectMemberRequest(Guid UserId, Enum.ProjectRole Role);
