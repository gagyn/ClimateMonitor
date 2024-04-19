using ClimateMonitor.Application.Models;
using MediatR;

namespace ClimateMonitor.Application.Commands;

public record UpdateSensorConfigurationCommand(
    Guid DeviceId,
    Guid SensorId,
    bool Activate,
    string Pin,
    string FrequencyCron,
    SensorType SensorType) : IRequest;