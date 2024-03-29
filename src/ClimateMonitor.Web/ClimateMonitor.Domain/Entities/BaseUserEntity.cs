using Microsoft.AspNetCore.Identity;

namespace ClimateMonitor.Domain.Entities;

public class BaseUserEntity : IdentityUser<Guid>
{
    private BaseUserEntity()
    {
    }

    private BaseUserEntity(Guid id, string username) : base(username)
    {
        Id = id;
    }

    internal static BaseUserEntity Create(string username) => new(Guid.NewGuid(), username);
    internal static BaseUserEntity Create(Func<Guid, string> usernameBasedOnIdFactory)
    {
        var id = Guid.NewGuid();
        return new BaseUserEntity(id, usernameBasedOnIdFactory(id));
    }
}