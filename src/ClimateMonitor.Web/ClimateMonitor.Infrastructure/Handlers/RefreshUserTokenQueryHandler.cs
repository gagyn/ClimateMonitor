using System.Security.Claims;
using ClimateMonitor.Application.Queries;
using ClimateMonitor.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ClimateMonitor.Infrastructure.Handlers;

public class RefreshUserTokenQueryHandler(
    TimeProvider timeProvider,
    IOptionsMonitor<BearerTokenOptions> optionsMonitor,
    SignInManager<BaseUserEntity> signInManager) : IRequestHandler<RefreshUserTokenQuery, ClaimsPrincipal>
{
    private readonly TimeProvider timeProvider = timeProvider;
    private readonly IOptionsMonitor<BearerTokenOptions> optionsMonitor = optionsMonitor;
    private readonly SignInManager<BaseUserEntity> signInManager = signInManager;

    public async Task<ClaimsPrincipal> Handle(RefreshUserTokenQuery request, CancellationToken cancellationToken)
    {
        var identityBearerOptions = optionsMonitor.Get(IdentityConstants.BearerScheme);
        var refreshTokenProtector = identityBearerOptions.RefreshTokenProtector
            ?? throw new ArgumentException($"{nameof(identityBearerOptions.RefreshTokenProtector)} is null", nameof(optionsMonitor));

        var refreshTicket = refreshTokenProtector.Unprotect(request.RefreshToken);

        // Reject the /refresh attempt with a 401 if the token expired or the security stamp validation fails
        if (refreshTicket?.Properties?.ExpiresUtc is not { } expiresUtc
            || timeProvider.GetUtcNow() >= expiresUtc
            || await signInManager.ValidateSecurityStampAsync(refreshTicket.Principal) is not { } user)
        {
            throw new UnauthorizedAccessException();
        }
        var newPrincipal = await signInManager.CreateUserPrincipalAsync(user);
        return newPrincipal;
    }
}
