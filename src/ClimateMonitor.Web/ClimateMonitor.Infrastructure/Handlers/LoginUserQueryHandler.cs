using System.Security.Claims;
using ClimateMonitor.Application.Queries;
using ClimateMonitor.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ClimateMonitor.Infrastructure.Handlers;

public class LoginUserQueryHandler(
    UserManager<BaseUserEntity> userManager,
    TimeProvider timeProvider,
    IUserClaimsPrincipalFactory<BaseUserEntity> claimsFactory) : IRequestHandler<LoginUserQuery, ClaimsPrincipal>
{
    private readonly UserManager<BaseUserEntity> userManager = userManager;
    private readonly TimeProvider timeProvider = timeProvider;
    private readonly IUserClaimsPrincipalFactory<BaseUserEntity> claimsFactory = claimsFactory;

    public async Task<ClaimsPrincipal> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.Username);
        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new UnauthorizedAccessException();
        }
        var claimsPrincipal = await claimsFactory.CreateAsync(user);
        return claimsPrincipal;
    }
}