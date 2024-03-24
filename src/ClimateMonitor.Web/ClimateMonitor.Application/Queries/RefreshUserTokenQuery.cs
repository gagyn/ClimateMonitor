using MediatR;
using System.Security.Claims;

namespace ClimateMonitor.Application.Queries;

public record RefreshUserTokenQuery(string RefreshToken) : IRequest<ClaimsPrincipal>;