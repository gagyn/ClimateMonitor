using ClimateMonitor.Application.Abstraction;
using ClimateMonitor.Application.Authorization;
using ClimateMonitor.Application.Commands;
using ClimateMonitor.Application.Extensions;
using ClimateMonitor.Domain.Entities;
using ClimateMonitor.Domain.Repositories;
using MediatR;

namespace ClimateMonitor.Application.Handlers;

public class UpdateSensorConfigurationCommandHandler(
    IDeviceRepository deviceRepository,
    TimeProvider timeProvider,
    IUserContext userContext,
    IDeviceConnection deviceConnection) : IRequestHandler<UpdateSensorConfigurationCommand>
{
    private readonly IDeviceRepository deviceRepository = deviceRepository;
    private readonly TimeProvider timeProvider = timeProvider;
    private readonly IUserContext userContext = userContext;
    private readonly IDeviceConnection deviceConnection = deviceConnection;

    public async Task Handle(UpdateSensorConfigurationCommand request, CancellationToken cancellationToken)
    {
        var device = await deviceRepository.FindOrThrow(request.DeviceId, userContext.Id, cancellationToken);
        var sensorConfiguration = device.SensorConfigurations.FirstOrDefault(x => x.SensorId == request.SensorId)
                                  ?? throw new Exception("Sensor doesn't exist.");

        sensorConfiguration.Update(
            request.Pin,
            Enum.Parse<SensorTypeEntity>(request.SensorType.ToString()),
            request.Activate,
            request.FrequencyCron,
            timeProvider,
            userContext.UserName);

        await deviceRepository.SaveChanges(cancellationToken);
        await deviceConnection.SendUpdatedConfiguration(device.ToDeviceConfiguration());
    }
}