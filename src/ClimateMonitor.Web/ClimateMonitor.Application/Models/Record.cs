namespace ClimateMonitor.Application.Models;

public record Record(Guid SensorId, decimal Temperature, decimal Humidity, DateTime ReadAt);
