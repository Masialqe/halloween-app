using HalloweenApp.Domain.Events;
using HalloweenApp.Domain.Visitors;

namespace HalloweenApp.Application.Commands.Visitors
{
    public sealed class EventState
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Visitor> Visitors { get; set; } = new();
        public bool HasStarted { get; set; }
        public bool HasEnded { get; set; }

        public static EventState FromDomain(Event domainEvent)
        {
            var newEventState = new EventState();
            newEventState.Id = domainEvent.Id;
            newEventState.Name = domainEvent.Name;
            newEventState.Description = domainEvent.Description;
            newEventState.HasStarted = domainEvent.HasStarted;
            newEventState.HasEnded = domainEvent.HasEnded;
            newEventState.Visitors = new List<Visitor>(domainEvent.Visitors);

            return newEventState;
        }

    }

    public sealed class ConnectEventToHubHandler
    {
    }
}
