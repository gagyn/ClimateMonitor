﻿namespace ClimateMonitor.Domain.Entities;
public class UserEntity : BaseEntity
{
    public Guid Id { get; }
    public BaseUserEntity BaseUser { get; private set; }
    public IList<DeviceEntity> Devices { get; private set; } = [];

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private UserEntity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    private UserEntity(BaseUserEntity baseUser, TimeProvider timeProvider, string createdBy) : base(timeProvider, createdBy)
    {
        BaseUser = baseUser;
    }

    public static UserEntity Create(string username, TimeProvider timeProvider, string createdBy)
        => new(BaseUserEntity.Create(username), timeProvider, createdBy);
}
