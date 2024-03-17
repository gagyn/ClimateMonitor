using ClimateMonitor.Application.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ClimateMonitor.Infrastructure.Authorization;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public string UserName => httpContextAccessor.HttpContext?.User?.Identity?.Name ?? string.Empty;

    public Guid? DeviceId => DeviceIdClaim == null ? Guid.Empty : Guid.Parse(DeviceIdClaim.Value);

    private Claim? DeviceIdClaim => Claims?.FirstOrDefault(x => x.Type == DeviceIdClaimName);

    public Role Role => Enum.Parse<Role>(Claims!.First(x => x.Type == ClaimTypes.Role).Value);

    private const string DeviceIdClaimName = "deviceId";
    private IEnumerable<Claim>? Claims => httpContextAccessor.HttpContext?.User.Claims;
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
}