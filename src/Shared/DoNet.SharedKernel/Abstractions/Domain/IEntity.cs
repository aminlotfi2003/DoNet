namespace DoNet.SharedKernel.Abstractions.Domain;

public interface IEntity
{
    Guid Id { get; set; }
    byte[] RowVersion { get; set; }
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset? UpdatedAt { get; set; }
}
