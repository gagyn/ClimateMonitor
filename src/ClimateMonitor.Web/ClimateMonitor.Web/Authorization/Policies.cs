using ClimateMonitor.Application.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace ClimateMonitor.Web.Authorization;

public static class Policies
{
    public const string User = nameof(User);
    public const string Device = nameof(Device);

    public static AuthorizationOptions AddPolicies(this AuthorizationOptions options) => options
        .AddRoles(User, Role.User)
        .AddRoles(Device, Role.Device);

    private static AuthorizationOptions AddRoles(this AuthorizationOptions options, string policyName, params Role[] userRoles)
    {
        var roleNames = userRoles.Select(x => x.ToString()).ToArray();
        options.AddPolicy(policyName, x => x.RequireRole(roleNames));
        return options;
    }
}