using ClimateMonitor.Application.Authorization;

namespace ClimateMonitor.Application.Models;

public record UserDetails(string Username, Guid Id, Role Role);