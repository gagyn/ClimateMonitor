namespace ClimateMonitor.Domain.Entities;

public class SensorConfigurationEntity : BaseEntity
{
    public Guid SensorId { get; private set; }
    public string Pin { get; private set; }
    public SensorTypeEntity SensorType { get; private set; }

    private SensorConfigurationEntity(
        Guid sensorId,
        string pin,
        SensorTypeEntity sensorType,
        TimeProvider timeProvider,
        string createdBy) : base(timeProvider, createdBy)
    {
        SensorId = sensorId;
        Pin = pin;
        SensorType = sensorType;
    }

    public static SensorConfigurationEntity Create(
        Guid sensorId,
        string pin,
        SensorTypeEntity sensorType,
        TimeProvider timeProvider,
        string createdBy)
        => new(sensorId, pin, sensorType, timeProvider, createdBy);

    public void Update(string pin, SensorTypeEntity sensorType, TimeProvider timeProvider, string updatedBy)
    {
        Pin = pin;
        SensorType = sensorType;
        SetUpdated(timeProvider, updatedBy);
    }
}
