using ClimateMonitor.Application.Models;
using MediatR;

namespace ClimateMonitor.Application.Commands;

public record AddSensorConfigurationCommand(
    Guid DeviceId,
    string Pin,
    SensorType SensorType,
    string FrequencyCron) : IRequest<Guid>;