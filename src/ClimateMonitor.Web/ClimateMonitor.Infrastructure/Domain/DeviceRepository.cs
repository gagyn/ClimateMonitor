using ClimateMonitor.Domain.Entities;
using ClimateMonitor.Domain.Repositories;
using ClimateMonitor.Infrastructure.Abstractions;
using ClimateMonitor.Infrastructure.Database;
using ClimateMonitor.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ClimateMonitor.Infrastructure.Domain;

public class DeviceRepository(ClimateDbContext dbContext) : BaseRepository(dbContext), IDeviceRepository
{
    public Task<DeviceEntity> FindOrThrow(Guid deviceId, Guid ownerUserId, CancellationToken cancellationToken)
        => dbContext.Devices
            .Include(x => x.SensorConfigurations)
            .Where(x => x.UserOwnerId == ownerUserId)
            .FindOrThrow(x => x.DeviceId, deviceId, cancellationToken);

    public void Add(DeviceEntity entity)
        => dbContext.Devices.Add(entity);
}
