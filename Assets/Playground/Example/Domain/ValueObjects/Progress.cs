namespace Playground.Example.Domain.ValueObjects
{
    public struct Progress
    {
        public readonly int Current;
        public readonly int Total;

        public Progress(int current, int total)
        {
            Current = current;
            Total = total;
        }

        public static Progress Empty => new Progress(0, 100);

        public Progress Increment() => new Progress(Current + 1, Total);
        
        public override bool Equals(object obj) => obj is Progress other && Equals(other);

        bool Equals(Progress other) => Current == other.Current && Total == other.Total;
    }
}