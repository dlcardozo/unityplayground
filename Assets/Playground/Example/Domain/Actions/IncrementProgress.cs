using Playground.Example.Domain.ValueObjects;
using UniRx;

namespace Playground.Example.Domain.Actions
{
    public class IncrementProgress
    {
        readonly ISubject<Progress> onProgressUpdated;

        public IncrementProgress(ISubject<Progress> onProgressUpdated)
        {
            this.onProgressUpdated = onProgressUpdated;
        }

        public void Do(int lastProgress) => onProgressUpdated.OnNext(new Progress(lastProgress + 1, 999));
    }
}