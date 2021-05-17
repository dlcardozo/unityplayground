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
    }
}