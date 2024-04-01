using ClimateMonitor.Application.Authorization;
using ClimateMonitor.Application.Models;
using ClimateMonitor.Application.Queries;
using MediatR;

namespace ClimateMonitor.Infrastructure.Handlers;

public class FindMyUserQueryHandler(IUserContext userContext) : IRequestHandler<FindMyUserQuery, UserDetails>
{
    public Task<UserDetails> Handle(FindMyUserQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new UserDetails(userContext.UserName, userContext.Id, userContext.Role));
    }
}