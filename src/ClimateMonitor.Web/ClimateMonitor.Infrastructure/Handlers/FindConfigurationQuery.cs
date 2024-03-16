using ClimateMonitor.Application.Models;
using MediatR;

namespace ClimateMonitor.Infrastructure.Handlers;

public record FindConfigurationQuery(Guid DeviceId) : IRequest<DeviceConfiguration>;