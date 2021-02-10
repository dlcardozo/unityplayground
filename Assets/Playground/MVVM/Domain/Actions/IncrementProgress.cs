using Playground.MVVM.Domain.ValueObjects;
using UniRx;

namespace Playground.MVVM.Domain.Actions
{
    public class IncrementProgress
    {
        readonly ISubject<Progress> onProgressUpdated;

        public IncrementProgress(ISubject<Progress> onProgressUpdated)
        {
            this.onProgressUpdated = onProgressUpdated;
        }

        public void Do() => onProgressUpdated.OnNext(new Progress(45, 999));
    }
}