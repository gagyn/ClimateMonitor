using ClimateMonitor.Application.Authorization;
using ClimateMonitor.Application.Commands;
using ClimateMonitor.Application.Models;
using ClimateMonitor.Domain.Entities;
using ClimateMonitor.Domain.Repositories;
using MediatR;

namespace ClimateMonitor.Application.Handlers;

public class AddSensorConfigurationCommandHandler(
    IDeviceRepository deviceRepository,
    TimeProvider timeProvider,
    IUserContext userContext) : IRequestHandler<AddSensorConfigurationCommand, Guid>
{
    private readonly IDeviceRepository deviceRepository = deviceRepository;
    private readonly TimeProvider timeProvider = timeProvider;
    private readonly IUserContext userContext = userContext;
    
    public async Task<Guid> Handle(AddSensorConfigurationCommand request, CancellationToken cancellationToken)
    {
        var device = await deviceRepository.FindOrThrow(request.DeviceId, userContext.Id, cancellationToken);
        var sensorConfiguration = SensorConfigurationEntity.Create(
            request.DeviceId,
            request.Pin,
            Enum.Parse<SensorTypeEntity>(request.SensorType.ToString()),
            request.FrequencyCron,
            timeProvider,
            userContext.UserName);
        
        device.AddSensor(sensorConfiguration, timeProvider, userContext.UserName);
        await deviceRepository.SaveChanges(cancellationToken);
        return sensorConfiguration.SensorId;
    }
}