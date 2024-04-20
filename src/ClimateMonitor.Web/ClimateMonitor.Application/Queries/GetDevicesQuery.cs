using ClimateMonitor.Application.Models;
using MediatR;

namespace ClimateMonitor.Application.Queries;

public record GetDevicesQuery : IRequest<IEnumerable<DeviceConfiguration>>;