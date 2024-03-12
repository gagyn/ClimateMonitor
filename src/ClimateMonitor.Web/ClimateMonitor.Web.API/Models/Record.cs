namespace ClimateMonitor.Web.API.Models;

public record Record(Guid SensorId, decimal Temperature, decimal Humidity, DateTime ReadAt);
