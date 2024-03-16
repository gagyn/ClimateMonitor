using System.Security.Claims;
using ClimateMonitor.Application.Authorization;
using Microsoft.AspNetCore.Http;

namespace ClimateMonitor.Infrastructure.Authorization;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public string UserName => httpContextAccessor.HttpContext?.User?.Identity?.Name ?? string.Empty;

    public Guid? DeviceId => DeviceIdClaim == null ? Guid.Empty : Guid.Parse(DeviceIdClaim.Value);

    private Claim? DeviceIdClaim => httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == DeviceIdClaimName);
    private const string DeviceIdClaimName = "deviceId";
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
}