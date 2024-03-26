using MediatR;

namespace ClimateMonitor.Application.Commands;
public record RegisterDeviceCommand(Guid UserId) : IRequest<Guid>;
