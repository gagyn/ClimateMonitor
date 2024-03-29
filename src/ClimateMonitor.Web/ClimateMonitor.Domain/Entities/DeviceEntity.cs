﻿namespace ClimateMonitor.Domain.Entities;

public class DeviceEntity : BaseEntity
{
    public Guid DeviceId { get; private set; }
    public string Name { get; private set; }
    public Guid UserOwnerId { get; private set; }
    public bool IsActive { get; private set; }
    public IReadOnlyList<SensorConfigurationEntity> SensorConfigurations => sensorConfigurations;

    private readonly List<SensorConfigurationEntity> sensorConfigurations = [];

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private DeviceEntity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    private DeviceEntity(
        string name,
        Guid deviceId,
        Guid userOwnerId,
        TimeProvider timeProvider,
        string createdBy) : base(timeProvider, createdBy)
    {
        Name = name;
        DeviceId = deviceId;
        UserOwnerId = userOwnerId;
        IsActive = true;
    }

    public static DeviceEntity Create(
        Guid deviceId,
        Guid userOwnerId,
        TimeProvider timeProvider,
        string createdBy)
        => new(name: "New device", deviceId, userOwnerId, timeProvider, createdBy);

    public void Update(string name, bool activate, TimeProvider timeProvider, string updatedBy)
    {
        Name = name;
        IsActive = activate;
        SetUpdated(timeProvider, updatedBy);
    }

    public void AddSensor(SensorConfigurationEntity sensorConfiguration, TimeProvider timeProvider, string updatedBy)
    {
        sensorConfigurations.Add(sensorConfiguration);
        SetUpdated(timeProvider, updatedBy);
    }

    public void SetActive(bool activate, TimeProvider timeProvider, string updatedBy)
    {
        IsActive = activate;
        SetUpdated(timeProvider, updatedBy);
    }
}
