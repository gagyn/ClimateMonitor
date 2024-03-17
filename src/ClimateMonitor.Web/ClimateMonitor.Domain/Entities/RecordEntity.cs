namespace ClimateMonitor.Domain.Entities;

public class RecordEntity : BaseEntity
{
    public int Id { get; }
    public Guid DeviceId { get; private set; }
    public Guid SensorId { get; private set; }
    public DateTimeOffset ReadAt { get; private set; }
    public double? Temperature { get; private set; }
    public double? Humidity { get; private set; }

    private RecordEntity()
    {
    }

    private RecordEntity(
        Guid deviceId,
        Guid sensorId,
        DateTimeOffset readAt,
        double? temperature,
        double? humidity,
        TimeProvider timeProvider,
        string createdBy) : base(timeProvider, createdBy)
    {
        DeviceId = deviceId;
        SensorId = sensorId;
        ReadAt = readAt;
        Temperature = temperature;
        Humidity = humidity;
    }

    public static RecordEntity Create(
        Guid deviceId,
        Guid sensorId,
        DateTimeOffset readAt,
        double? temperature,
        double? humidity,
        TimeProvider timeProvider,
        string createdBy)
        => new(deviceId, sensorId, readAt, temperature, humidity, timeProvider, createdBy);
}
