using Playground.Example.Domain.Actions;
using Playground.Example.Domain.ValueObjects;
using UniRx;

namespace Playground.Example
{
    public static class Context
    {
        public static void Start()
        {
            var onProgressUpdated = new Subject<Progress>();
            new IncrementProgress(onProgressUpdated);
        }
    }
}