using ClimateMonitor.Application.Authorization;
using ClimateMonitor.Application.Models;
using ClimateMonitor.Application.Queries;
using ClimateMonitor.Domain.Entities;
using ClimateMonitor.Infrastructure.Database;
using ClimateMonitor.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClimateMonitor.Infrastructure.Handlers;

public class FindConfigurationQueryHandler(ClimateDbContext climateDbContext, IUserContext userContext) : IRequestHandler<FindConfigurationQuery, DeviceConfiguration>
{
    public async Task<DeviceConfiguration> Handle(FindConfigurationQuery request, CancellationToken cancellationToken)
    {
        var deviceId = userContext.Role == Role.Device ? userContext.Id : request.DeviceId;
        var deviceConfig = await climateDbContext.Devices
            .Include(x => x.SensorConfigurations)
            .Where(x => x.UserOwnerId == userContext.Id || x.DeviceId == deviceId)
            .FirstOrThrow(deviceId, cancellationToken);

        return deviceConfig.ToDeviceConfiguration();
    }
}