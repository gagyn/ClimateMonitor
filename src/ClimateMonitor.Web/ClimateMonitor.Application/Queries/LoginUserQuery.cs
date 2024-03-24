using MediatR;
using System.Security.Claims;

namespace ClimateMonitor.Application.Queries;

public record LoginUserQuery(string Username, string Password) : IRequest<ClaimsPrincipal>;