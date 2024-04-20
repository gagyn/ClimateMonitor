using ClimateMonitor.Application.Models;
using ClimateMonitor.Domain.Entities;

namespace ClimateMonitor.Infrastructure.Extensions;

public static class DeviceConfigurationExtensions
{
    public static DeviceConfiguration ToDeviceConfiguration(this DeviceEntity entity)
    {
        var readingFrequencyCrons = entity.SensorConfigurations
            .ToDictionary(x => x.SensorId, x => x.FrequencyCron);

        var pinsDht11 = entity.SensorConfigurations
            .Where(x => x.SensorType == SensorTypeEntity.DHT11)
            .ToDictionary(x => x.SensorId, x => x.Pin);

        var pinsDht22 = entity.SensorConfigurations
            .Where(x => x.SensorType == SensorTypeEntity.DHT22)
            .ToDictionary(x => x.SensorId, x => x.Pin);

        var pinsDallas18b20 = entity.SensorConfigurations
            .Where(x => x.SensorType == SensorTypeEntity.Dallas18b20)
            .ToDictionary(x => x.SensorId, x => x.Pin);

        return new(entity.DeviceId, readingFrequencyCrons, pinsDht11, pinsDht22, pinsDallas18b20);
    }
}