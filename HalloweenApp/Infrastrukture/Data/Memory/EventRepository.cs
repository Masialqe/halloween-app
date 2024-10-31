using HalloweenApp.Application.Abstractions;
using HalloweenApp.Domain.Events;
using HalloweenApp.Domain.Visitors;

namespace HalloweenApp.Infrastrukture.Data.Memory
{
    public sealed class EventRepository : IEventRepository
    {
        private List<Event> _events;

        public EventRepository()
        {
            _events = new List<Event>();
        }

        public Task<IEnumerable<Event>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_events.AsEnumerable());
        }

        public Task<Event?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_events.FirstOrDefault(x => x.Id == id));
        }

        public Task SaveAsync(Event newEvent, CancellationToken cancellationToken = default)
        {
            _events.Add(newEvent);

            return Task.CompletedTask;
        }

        public Task AddVisitorAsync(Guid eventId, Visitor visitor)
        {
            var targetEvent = _events.First(x => x.Id == eventId);
            targetEvent.AddVisitor(visitor);

            return Task.CompletedTask;
        }

        public Task StartEventAsync(Guid eventId, CancellationToken cancellationToken = default)
        {
            var targetEvent = _events.First(x => x.Id == eventId);
            targetEvent.StartEvent();

            return Task.CompletedTask;
        }

        public Task EndEventAsync(Guid eventId, CancellationToken cancellationToken = default)
        {
            var targetEvent = _events.First(x => x.Id == eventId);
            targetEvent.EndEvent();

            return Task.CompletedTask;
        }

        public Task ResetEventAsync(Guid eventId, CancellationToken cancellationToken = default)
        {
            var targetEvent = _events.First(x => x.Id == eventId);
            targetEvent.ResetEvent();

            return Task.CompletedTask;
        }

        public Task<Visitor[]> GetEventVisitors(Guid eventId)
        {
            var targetEvent = _events.First(x => x.Id == eventId);

            return Task.FromResult(targetEvent.Visitors.ToArray());
        }

        public Task AddVoteToEventAsync(Guid eventId)
        {
            var targetEvent = _events.First(x => x.Id == eventId);

            targetEvent.AddVote();

            return Task.CompletedTask;
        }

        public Task RemoveAllEventsAsync()
        {
            _events.Clear();
            return Task.CompletedTask;
        }
    }
}
