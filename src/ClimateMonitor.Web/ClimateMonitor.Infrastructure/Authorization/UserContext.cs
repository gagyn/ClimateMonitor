using ClimateMonitor.Application.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ClimateMonitor.Infrastructure.Authorization;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid Id => Guid.Parse(Claims!.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
    public string UserName => httpContextAccessor.HttpContext?.User?.Identity?.Name ?? string.Empty;

    public Role Role => Enum.Parse<Role>(Claims!.First(x => x.Type == ClaimTypes.Role).Value);

    private IEnumerable<Claim>? Claims => httpContextAccessor.HttpContext?.User.Claims;
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
}