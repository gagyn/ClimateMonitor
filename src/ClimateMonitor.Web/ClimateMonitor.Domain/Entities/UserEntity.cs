using Microsoft.AspNetCore.Identity;

namespace ClimateMonitor.Domain.Entities;

public class UserEntity : IdentityUser<Guid>
{
    private UserEntity()
    {
    }
    
    private UserEntity(string username) : base(username)
    {
    }
    
    public static UserEntity Create(string username) => new(username);
}