namespace ClimateMonitor.Domain.Entities;

public class DeviceUserEntity : BaseEntity
{
    public Guid Id { get; }
    public BaseUserEntity BaseUser { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private DeviceUserEntity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    private DeviceUserEntity(BaseUserEntity baseUser, TimeProvider timeProvider, string createdBy) : base(timeProvider, createdBy)
    {
        BaseUser = baseUser;
    }

    public static DeviceUserEntity Create(string deviceName, TimeProvider timeProvider, string createdBy)
        => new(BaseUserEntity.Create(deviceName), timeProvider, createdBy);
}