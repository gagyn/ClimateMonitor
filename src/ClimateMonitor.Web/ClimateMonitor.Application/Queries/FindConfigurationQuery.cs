using ClimateMonitor.Application.Models;
using MediatR;

namespace ClimateMonitor.Application.Queries;

public record FindConfigurationQuery(Guid DeviceId) : IRequest<DeviceConfiguration>;