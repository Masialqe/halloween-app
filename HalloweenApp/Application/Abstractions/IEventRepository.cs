using HalloweenApp.Domain.Events;
using HalloweenApp.Domain.Visitors;

namespace HalloweenApp.Application.Abstractions
{
    public interface IEventRepository
    {
        Task SaveAsync(
            Event newEvent, 
            CancellationToken cancellationToken = default);
        Task<Event?> GetByIdAsync(
            Guid id, 
            CancellationToken cancellationToken = default);
        Task<IEnumerable<Event>> GetAllAsync(
            CancellationToken cancellationToken = default);
        Task AddVisitorAsync(
            Guid eventId, 
            Visitor visitor);
        Task StartEventAsync(
            Guid eventId, 
            CancellationToken cancellationToken = default);
        Task EndEventAsync(
            Guid eventId, 
            CancellationToken cancellationToken = default);
        Task ResetEventAsync(
            Guid eventId, 
            CancellationToken cancellationToken = default);
        Task<Visitor[]> GetEventVisitors(
            Guid eventId);
        Task AddVoteToEventAsync(
            Guid eventId);
        Task RemoveAllEventsAsync();


    }
}
