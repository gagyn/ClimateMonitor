using ClimateMonitor.Application.Models;
using ClimateMonitor.Domain.Entities;
using ClimateMonitor.Infrastructure.Database;
using ClimateMonitor.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClimateMonitor.Infrastructure.Handlers;

public class FindConfigurationQueryHandler(ClimateDbContext climateDbContext) : IRequestHandler<FindConfigurationQuery, DeviceConfiguration>
{
    public async Task<DeviceConfiguration> Handle(FindConfigurationQuery request, CancellationToken cancellationToken)
    {
        var deviceConfig = await climateDbContext.DeviceConfigurations
            .Include(x => x.SensorConfigurations)
            .FindOrThrow(x => x.DeviceId, request.DeviceId, cancellationToken);

        var readingFrequencyCrons = deviceConfig.SensorConfigurations
            .ToDictionary(x => x.SensorId, x => x.FrequencyCron);

        var pinsDht11 = deviceConfig.SensorConfigurations
            .Where(x => x.SensorType == SensorTypeEntity.DHT11)
            .ToDictionary(x => x.SensorId, x => x.Pin);

        var pinsDht22 = deviceConfig.SensorConfigurations
            .Where(x => x.SensorType == SensorTypeEntity.DHT22)
            .ToDictionary(x => x.SensorId, x => x.Pin);

        var pinsDallas18b20 = deviceConfig.SensorConfigurations
            .Where(x => x.SensorType == SensorTypeEntity.Dallas18b20)
            .ToDictionary(x => x.SensorId, x => x.Pin);

        return new(readingFrequencyCrons, pinsDht11, pinsDht22, pinsDallas18b20);
    }
}