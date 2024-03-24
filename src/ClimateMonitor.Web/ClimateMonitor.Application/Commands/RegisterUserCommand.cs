using MediatR;

namespace ClimateMonitor.Application.Commands;
public record RegisterUserCommand(string Username, string Password) : IRequest;