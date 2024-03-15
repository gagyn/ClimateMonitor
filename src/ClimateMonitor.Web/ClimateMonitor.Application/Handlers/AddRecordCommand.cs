using MediatR;

namespace ClimateMonitor.Application.Handlers;

public record AddRecordCommand(
    Guid SensorId,
    DateTimeOffset ReadAt,
    double? Temperature,
    double? Humidity) : IRequest;