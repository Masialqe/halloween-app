using HalloweenApp.Application.Abstractions;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using HalloweenApp.Domain.Visitors;
using HalloweenApp.Domain.Events;


namespace HalloweenApp.Infrastrukture.Reactivity
{
    public sealed class EventHub
        (IEventRepository _eventRepository): Hub
    {
        private static ConcurrentDictionary<Guid, Event> _events = new();

        public async Task TrackEvent(Guid eventId)
        {
            var savedEvent = await _eventRepository.GetByIdAsync(eventId);
            await Clients.Caller.SendAsync("SetEventState", savedEvent);

        }

        public async Task AddVisitor(Guid Id, Visitor visitor)
        {
            var currentEventState = await _eventRepository.GetByIdAsync(Id);
            await _eventRepository.AddVisitorAsync(Id, visitor);
            await Clients.Others.SendAsync("UpdateVisitors", visitor);
        }

        public async Task StartEvent(Guid Id)
        {
            await _eventRepository.StartEventAsync(Id);
            await Clients.Others.SendAsync("EventStateStart");
        }

        public async Task EndEvent(Guid Id)
        {
            await _eventRepository.EndEventAsync(Id);
            await Clients.Others.SendAsync("EventStateEnd");
        }

        public async Task ResetEvent(Guid Id)
        {
            await _eventRepository.ResetEventAsync(Id);
            await Clients.Others.SendAsync("EventStateReset");
        }


        public async Task AddVisitorPoints(Guid eventId, Dictionary<string, int> points)
        {

            var savedEvent = await _eventRepository.GetByIdAsync(eventId);

            if(savedEvent != null)
            {
                savedEvent.AddVote();

                foreach (var visitor in points.Keys)
                {
                    var visitorToAssignPoints = savedEvent.Visitors.First(x => x.Name == visitor);
                    visitorToAssignPoints.AddPoints(points[visitor]);
                }

                if(savedEvent.Votes == savedEvent.Visitors.Count)
                {
                    savedEvent.EndEvent();
                    await Clients.All.SendAsync("SetEventState", savedEvent);
                }
            }

        }

        public async Task GetVisitorsScore(Guid eventId)
        {
            var currentVisitorsState = await _eventRepository.GetEventVisitors(eventId);
            await Clients.Caller.SendAsync("SetVisitorsWithScore", currentVisitorsState);
        }

        public async Task UpdateVisitorsState(Guid eventId)
        {
            if (_events.TryGetValue(eventId, out var matchingEvent))
            {
               var result = matchingEvent.Visitors.ToArray();
               await Clients.All.SendAsync("SetVisitorsState", result);
            }
        }
    }
}
