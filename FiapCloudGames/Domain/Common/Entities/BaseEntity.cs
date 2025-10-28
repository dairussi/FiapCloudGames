namespace FiapCloudGames.Domain.Common.Entities;

public abstract class BaseEntity
{
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public int CreatedBy { get; protected internal set; }
}