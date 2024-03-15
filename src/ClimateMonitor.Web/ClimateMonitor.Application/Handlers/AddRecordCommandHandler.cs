using ClimateMonitor.Domain.Entities;
using ClimateMonitor.Domain.Repositories;
using MediatR;

namespace ClimateMonitor.Application.Handlers;

public class AddRecordCommandHandler(IRecordRepository recordRepository, TimeProvider timeProvider) : IRequestHandler<AddRecordCommand>
{
    private readonly IRecordRepository recordRepository = recordRepository;
    private readonly TimeProvider timeProvider = timeProvider;
    
    public async Task Handle(AddRecordCommand request, CancellationToken cancellationToken)
    {
        var deviceId = Guid.Empty; // todo: implement user context
        var record = RecordEntity.Create(deviceId, request.SensorId, request.ReadAt, request.Temperature,
            request.Humidity, timeProvider, "createdBy");// todo
        
        recordRepository.Add(record);
        await recordRepository.SaveChanges(cancellationToken);
    }
}