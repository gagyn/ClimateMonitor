using Microsoft.AspNetCore.Identity;

namespace ClimateMonitor.Domain.Entities;

public class BaseUserEntity : IdentityUser<Guid>
{
    private BaseUserEntity()
    {
    }

    private BaseUserEntity(string username) : base(username)
    {
    }

    internal static BaseUserEntity Create(string username) => new(username);
}