using ClimateMonitor.Domain.Entities;

namespace ClimateMonitor.Infrastructure.Exceptions;

public class EntityNotFoundException<T>(object id)
    : Exception($"{typeof(T).Name} entity with ID: {id} not found.")
    where T : class;