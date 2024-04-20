using ClimateMonitor.Application.Authorization;
using ClimateMonitor.Application.Models;
using ClimateMonitor.Application.Queries;
using ClimateMonitor.Infrastructure.Database;
using ClimateMonitor.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClimateMonitor.Infrastructure.Handlers;

public class GetDevicesQueryHandler(ClimateDbContext dbContext, IUserContext userContext)
    : IRequestHandler<GetDevicesQuery, IEnumerable<DeviceConfiguration>>
{
    public async Task<IEnumerable<DeviceConfiguration>> Handle(GetDevicesQuery request, CancellationToken cancellationToken)
    {
        var devices = await dbContext.Devices
            .Include(x => x.SensorConfigurations)
            .Where(x => x.UserOwnerId == userContext.Id)
            .Select(x => x.ToDeviceConfiguration())
            .ToListAsync(cancellationToken);

        return devices;
    }
}