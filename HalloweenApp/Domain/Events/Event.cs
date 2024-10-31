using HalloweenApp.Domain.Visitors;

namespace HalloweenApp.Domain.Events
{
    public sealed class Event
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; } = string.Empty;
        public string Description { get;} = string.Empty;

        //TODO ConcurentBag, private setter
        public List<Visitor> Visitors { get; set; } = new();


        private int _votes;
        public int Votes 
        {
            get => _votes;
            set => _votes = value;
        }

        //TODO private setter
        private bool _hasEnded;
        public bool HasEnded
        {
            get => _hasEnded;
            set => _hasEnded = value; 
        }

        private bool _hasStarted;
        public bool HasStarted
        {
            get => _hasStarted;
            set => _hasStarted = value;
        }

        private object _lock = new object();

        public Event(string name, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));

            ResetEvent();
        }

        public static Event Create(string name, string description)
            => new Event(name, description);

        public static Event CreateDefault()
            => new Event("default", "default");

        public void AddVisitor(Visitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException(nameof(visitor));

            if (Visitors.Contains(visitor))
                throw new VisitorAlreadySignedInException("This name already exists!");

            if (_hasEnded)
                throw new InvalidOperationException("Cannot add visitors to an event that has ended.");

            lock(_lock)
            {
                Visitors.Add(visitor);
            }
        }

        public void AddVote()
        {
            Interlocked.Add(ref _votes, 1);
        }

        public Visitor GetVisitor(string name)
            => Visitors.Where(x => x.Name == name).First();

        public void EndEvent()
            => _hasEnded = true;

        public void StartEvent()
            => _hasStarted = true;

        public void ResetEvent()
        {
            _hasEnded = false;
            _hasStarted = false;
            _votes = 0;
            Visitors = new ();
        }
    }

    //TODO - change to DTO for components
    public static class EventExtensions
    {
        public static Event Copy(this Event eventToCopy)
        {
            if(eventToCopy == null)
                throw new ArgumentNullException(nameof(eventToCopy));

            var newEvent = Event.Create(eventToCopy.Name, eventToCopy.Description);
            foreach (var visitor in eventToCopy.Visitors)
            {
                newEvent.AddVisitor(visitor);
            }

            if (eventToCopy.HasEnded)
                newEvent.EndEvent();

            if (eventToCopy.HasStarted)
                newEvent.StartEvent();

            return newEvent;
        }
    }
}
