namespace ClimateMonitor.Domain.Entities;

public class BaseEntity
{
    public DateTimeOffset CreatedAt { get; private set; }
    public string CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public string? UpdatedBy { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected BaseEntity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    protected BaseEntity(TimeProvider timeProvider, string createdBy)
    {
        CreatedAt = timeProvider.GetLocalNow();
        CreatedBy = createdBy;
    }

    protected void SetUpdated(TimeProvider timeProvider, string updatedBy)
    {
        UpdatedAt = timeProvider.GetLocalNow();
        UpdatedBy = updatedBy;
    }
}