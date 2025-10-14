using DoNet.Application.Abstractions.Services;

namespace DoNet.Infrastructure.Identity.Services;

public sealed class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
