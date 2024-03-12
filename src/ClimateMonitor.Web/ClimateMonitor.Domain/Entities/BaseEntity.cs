namespace ClimateMonitor.Domain.Entities;

public class BaseEntity
{
    public DateTimeOffset CreatedAt { get; private set; }
    public string CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public string? UpdatedBy { get; private set; }

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