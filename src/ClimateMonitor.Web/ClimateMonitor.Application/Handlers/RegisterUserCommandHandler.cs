using ClimateMonitor.Application.Authorization;
using ClimateMonitor.Application.Commands;
using ClimateMonitor.Application.Exceptions;
using ClimateMonitor.Domain.Entities;
using ClimateMonitor.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ClimateMonitor.Application.Handlers;

public class RegisterUserCommandHandler(
    UserManager<BaseUserEntity> userManager,
    TimeProvider timeProvider,
    IUserRepository userRepository) : IRequestHandler<RegisterUserCommand>
{
    private readonly UserManager<BaseUserEntity> userManager = userManager;
    private readonly TimeProvider timeProvider = timeProvider;
    private readonly IUserRepository userRepository = userRepository;

    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = UserEntity.Create(request.Username, timeProvider, request.Username);
        await userManager.SetUserNameAsync(user.BaseUser, request.Username);
        var result = await userManager.CreateAsync(user.BaseUser, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            throw new RegisterUserValidationException(string.Join(", ", errors));
        }

        userRepository.Add(user);
        await userRepository.SaveChanges(cancellationToken);

        await userManager.AddToRoleAsync(user.BaseUser, Role.User.ToString());
    }
}