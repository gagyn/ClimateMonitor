namespace ClimateMonitor.Application.Models;

public record DeviceConfiguration(
    Dictionary<Guid, string> ReadingFrequencyCrons,
    Dictionary<Guid, string> PinsDHT11,
    Dictionary<Guid, string> PinsDHT22,
    Dictionary<Guid, string> PinsDallas18b20);