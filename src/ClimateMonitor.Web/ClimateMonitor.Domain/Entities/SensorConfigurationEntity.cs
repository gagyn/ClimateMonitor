namespace ClimateMonitor.Domain.Entities;

public class SensorConfigurationEntity : BaseEntity
{
    public Guid SensorId { get; private set; }
    public string Pin { get; private set; }
    public SensorTypeEntity SensorType { get; private set; }
    public bool IsActive { get; private set; }
    public string FrequencyCron { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private SensorConfigurationEntity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    private SensorConfigurationEntity(
        Guid sensorId,
        string pin,
        SensorTypeEntity sensorType,
        string frequencyCron,
        TimeProvider timeProvider,
        string createdBy) : base(timeProvider, createdBy)
    {
        SensorId = sensorId;
        Pin = pin;
        SensorType = sensorType;
        IsActive = true;
        FrequencyCron = frequencyCron;
    }

    public static SensorConfigurationEntity Create(
        Guid sensorId,
        string pin,
        SensorTypeEntity sensorType,
        string frequencyCron,
        TimeProvider timeProvider,
        string createdBy)
        => new(sensorId, pin, sensorType, frequencyCron, timeProvider, createdBy);

    public void Update(string pin, SensorTypeEntity sensorType, bool activate, string frequencyCron, TimeProvider timeProvider, string updatedBy)
    {
        Pin = pin;
        SensorType = sensorType;
        IsActive = activate;
        FrequencyCron = frequencyCron;
        SetUpdated(timeProvider, updatedBy);
    }

    public void SetActive(bool activate, TimeProvider timeProvider, string updatedBy)
    {
        IsActive = activate;
        SetUpdated(timeProvider, updatedBy);
    }
}
