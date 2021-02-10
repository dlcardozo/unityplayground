using Playground.MVVM.Domain.Actions;
using Playground.MVVM.Domain.ValueObjects;
using Playground.MVVM.ViewModels;
using UniRx;

namespace Playground.MVVM
{
    public static class Context
    {
        public static ProgressViewModel ViewModel;

        public static void Start()
        {
            var onProgressUpdated = new Subject<Progress>();
            var incrementProgress = new IncrementProgress(onProgressUpdated);
        }
    }
}