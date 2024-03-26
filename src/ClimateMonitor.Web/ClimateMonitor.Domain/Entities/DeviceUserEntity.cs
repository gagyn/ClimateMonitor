namespace ClimateMonitor.Domain.Entities;

public class DeviceUserEntity : BaseEntity
{
    public Guid DeviceId { get; }
    public BaseUserEntity BaseUser { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private DeviceUserEntity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    private DeviceUserEntity(Guid deviceId, BaseUserEntity baseUser, TimeProvider timeProvider, string createdBy) : base(timeProvider, createdBy)
    {
        DeviceId = deviceId;
        BaseUser = baseUser;
    }

    public static DeviceUserEntity Create(TimeProvider timeProvider)
    {
        var deviceId = Guid.NewGuid();
        var username = $"Device {deviceId}";
        return new(
            deviceId,
            BaseUserEntity.Create(username),
            timeProvider,
            username);
    }
}