namespace ClimateMonitor.Web.API.Models;

public record SensorConfiguration(
    Dictionary<Guid, string> ReadingFrequencyCrons,
    Dictionary<Guid, int> PinsDHT11,
    Dictionary<Guid, int> PinsDHT22,
    Dictionary<Guid, int> PinsDallas18b20);
