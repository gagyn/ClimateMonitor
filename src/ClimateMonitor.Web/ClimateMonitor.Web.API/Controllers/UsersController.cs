using ClimateMonitor.Domain.Entities;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ClimateMonitor.Web.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(
    UserManager<BaseUserEntity> userManager,
    IUserClaimsPrincipalFactory<BaseUserEntity> claimsFactory,
    IOptionsMonitor<BearerTokenOptions> optionsMonitor,
    TimeProvider timeProvider,
    SignInManager<BaseUserEntity> signInManager) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var user = UserEntity.Create(command.Username, timeProvider, command.Username);
        await userManager.SetUserNameAsync(user.BaseUser, command.Username);
        var result = await userManager.CreateAsync(user.BaseUser, command.Password);

        if (result.Succeeded)
        {
            return Ok();
        }

        var errors = result.Errors.ToDictionary(e => e.Code, e => new[] { e.Description });
        return BadRequest(errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var user = await userManager.FindByNameAsync(command.Username);

        if (user is null || !await userManager.CheckPasswordAsync(user, command.Password))
        {
            return Unauthorized();
        }

        var claimsPrincipal = await claimsFactory.CreateAsync(user);

        var useCookies = false;
        var scheme = useCookies ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

        return SignIn(claimsPrincipal, authenticationScheme: scheme);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
    {
        var identityBearerOptions = optionsMonitor.Get(IdentityConstants.BearerScheme);
        var refreshTokenProtector = identityBearerOptions.RefreshTokenProtector
            ?? throw new ArgumentException($"{nameof(identityBearerOptions.RefreshTokenProtector)} is null", nameof(optionsMonitor));
        var refreshTicket = refreshTokenProtector.Unprotect(refreshRequest.RefreshToken);

        // Reject the /refresh attempt with a 401 if the token expired or the security stamp validation fails
        if (refreshTicket?.Properties?.ExpiresUtc is not { } expiresUtc
            || timeProvider.GetUtcNow() >= expiresUtc
            || await signInManager.ValidateSecurityStampAsync(refreshTicket.Principal) is not { } user)
        {
            return Challenge();
        }
        var newPrincipal = await signInManager.CreateUserPrincipalAsync(user);
        return SignIn(newPrincipal, authenticationScheme: IdentityConstants.BearerScheme);
    }
}

public record RegisterCommand(string Username, string Password);
public record LoginCommand(string Username, string Password);