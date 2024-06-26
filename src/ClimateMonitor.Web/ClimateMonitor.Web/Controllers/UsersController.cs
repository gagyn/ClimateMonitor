using ClimateMonitor.Application.Commands;
using ClimateMonitor.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClimateMonitor.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }

    [HttpPost("register-device")]
    public async Task<IActionResult> RegisterDevice([FromBody] RegisterDeviceCommand command)
        => Ok(await mediator.Send(command));

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserQuery query, [FromQuery] bool useCookies)
    {
        var claimsPrincipal = await mediator.Send(query);

        var scheme = useCookies ? CookieAuthenticationDefaults.AuthenticationScheme : IdentityConstants.BearerScheme;

        return SignIn(claimsPrincipal, authenticationScheme: scheme);
    }

    [HttpGet("logout")]
    public IActionResult Logout() => SignOut(CookieAuthenticationDefaults.AuthenticationScheme);

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshUserTokenQuery query)
    {
        var newPrincipal = await mediator.Send(query);
        return SignIn(newPrincipal, authenticationScheme: IdentityConstants.BearerScheme);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
        => Ok(await mediator.Send(new FindMyUserQuery()));
}