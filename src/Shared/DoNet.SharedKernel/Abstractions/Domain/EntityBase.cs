namespace DoNet.SharedKernel.Abstractions.Domain;

public abstract class EntityBase : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }

    protected void Touch() => UpdatedAt = DateTimeOffset.UtcNow;
}
