using MediatR;

namespace ClimateMonitor.Application.Commands;

public record AddRecordCommand(
    Guid SensorId,
    DateTimeOffset ReadAt,
    double? Temperature,
    double? Humidity) : IRequest;