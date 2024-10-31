namespace HalloweenApp.Domain.Visitors
{
    public sealed class Visitor
    {
        public string Name { get; }

        //TODO - private setter and DTO for transmition
        public int Points
        {
            get => _points;
            set => _points = value;
        }
        public bool HasVoted { get; set; } = false;

        private int _points;
        private List<int> _gainedPoints;
        private readonly object _lock;

        public Visitor(string name)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            Name = name;
            _points = 0;
            _gainedPoints = new();
            _lock = new();
        }

        public static Visitor Create(string name)
            => new Visitor(name);

        public void AddPoints(int points)
        {
            lock( _lock)
            {
                _points += points;
            }
        }

        public void EmptyScore()
        {
            lock(_lock)
            {
                _points = 0;
            }
        }

        public int GetScore()
        {
            return _points;
        }
    }
}
