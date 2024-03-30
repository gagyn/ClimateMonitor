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
    IDeviceRepository deviceRepository) : IRequestHandler<RegisterDeviceCommand, Guid>
{
    private readonly UserManager<BaseUserEntity> userManager = userManager;
    private readonly TimeProvider timeProvider = timeProvider;
    private readonly IUserRepository userRepository = userRepository;

    public async Task<Guid> Handle(RegisterDeviceCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindOrThrow(request.UserId, cancellationToken);
        var device = DeviceEntity.Create(user.Id, timeProvider, user.BaseUser.UserName!);
        //await userManager.SetUserNameAsync(deviceUser.BaseUser, request.Username);
        var result = await userManager.CreateAsync(device.BaseUser, password: device.DeviceId.ToString());

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            throw new RegisterUserValidationException(string.Join(", ", errors));
        }
        
        deviceRepository.Add(device);
        await userManager.AddToRoleAsync(device.BaseUser, Role.Device.ToString());
        await deviceRepository.SaveChanges(cancellationToken);
        return device.DeviceId;
    }
}