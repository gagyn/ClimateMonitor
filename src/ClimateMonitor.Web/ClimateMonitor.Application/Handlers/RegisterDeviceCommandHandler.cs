using ClimateMonitor.Application.Authorization;
using ClimateMonitor.Application.Commands;
using ClimateMonitor.Application.Exceptions;
using ClimateMonitor.Domain.Entities;
using ClimateMonitor.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ClimateMonitor.Application.Handlers;

public class RegisterDeviceCommandHandler(
    UserManager<BaseUserEntity> userManager,
    TimeProvider timeProvider,
    IUserRepository userRepository,
    IDeviceUserRepository deviceUserRepository,
    IDeviceRepository deviceRepository) : IRequestHandler<RegisterDeviceCommand, Guid>
{
    private readonly UserManager<BaseUserEntity> userManager = userManager;
    private readonly TimeProvider timeProvider = timeProvider;
    private readonly IUserRepository userRepository = userRepository;
    private readonly IDeviceUserRepository deviceUserRepository = deviceUserRepository;

    public async Task<Guid> Handle(RegisterDeviceCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindOrThrow(request.UserId, cancellationToken);
        var deviceUser = DeviceUserEntity.Create(timeProvider);
        //await userManager.SetUserNameAsync(deviceUser.BaseUser, request.Username);
        var result = await userManager.CreateAsync(deviceUser.BaseUser, password: deviceUser.DeviceId.ToString());

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            throw new RegisterUserValidationException(string.Join(", ", errors));
        }

        var device = DeviceEntity.Create(deviceUser.DeviceId, user.Id, timeProvider, deviceUser.BaseUser.UserName!);
        deviceUserRepository.Add(deviceUser);
        deviceRepository.Add(device);
        
        await deviceRepository.SaveChanges(cancellationToken);

        await userManager.AddToRoleAsync(deviceUser.BaseUser, Role.Device.ToString());
        return device.DeviceId;
    }
}