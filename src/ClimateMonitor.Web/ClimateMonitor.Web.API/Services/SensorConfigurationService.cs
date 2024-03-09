using ClimateMonitor.Web.API.Models;

namespace ClimateMonitor.Web.API.Services;

public class SensorConfigurationService
{
    private readonly static Guid sensorId = Guid.NewGuid();
    public readonly static Guid TestDeviceId = Guid.Parse("a22e59b7-aa2b-49df-9420-97d6e017fbb3");
    private readonly static Guid TestSensorId = Guid.NewGuid();
    private readonly static Guid TestSensorId2 = Guid.NewGuid();
    private static SensorConfiguration sensorConfig = new(
        new Dictionary<Guid, string>
        {
            { TestSensorId, "* * * * *" }, // Example CRON expression
            { TestSensorId2, "0 * * * *" }  // Another example CRON expression
        },
        new Dictionary<Guid, int>
        {
            { TestSensorId, 4 },  // Example pin configuration for DHT11 sensor
            { TestSensorId2, 17 }  // Another example pin configuration for DHT11 sensor
        },
        new Dictionary<Guid, int>
        {
            //{ Guid.NewGuid(), 23 },  // Example pin configuration for DHT22 sensor
            //{ Guid.NewGuid(), 24 }   // Another example pin configuration for DHT22 sensor
        },
        new Dictionary<Guid, int>
        {
            //{ Guid.NewGuid(), 5 },  // Example pin configuration for Dallas 18b20 sensor
            //{ Guid.NewGuid(), 6 }   // Another example pin configuration for Dallas 18b20 sensor
        }
    );
    private static readonly Dictionary<Guid, SensorConfiguration> deviceConfigurations = new()
    {
        {TestDeviceId, sensorConfig}
    };

    public SensorConfiguration GetConfiguration(Guid deviceId) => deviceConfigurations[deviceId];
}
